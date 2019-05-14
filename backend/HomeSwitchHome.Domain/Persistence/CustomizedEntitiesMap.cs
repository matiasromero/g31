using System;
using System.IO;
using System.Linq;
using HomeSwitchHome.Infrastructure.Domain;
using HomeSwitchHome.Domain.Base;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Domain.Enumerations;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using NHibernate.UserTypes;

namespace HomeSwitchHome.Domain.Persistence
{
    public class CustomizedEntitiesMap
    {
        public const int MaxLo = 100;

        public void Configure(Configuration configuration)
        {
            Configure(configuration, typeof(User).Assembly.GetExportedTypes().ToArray());
        }

        public void Configure(Configuration configuration, Type[] types)
        {
            var assemblies = types.Select(x => x.Assembly).Distinct().ToArray();

            var mapper = new ConventionModelMapper();

            var baseEntityType = typeof(IGenericEntity);
            var baseEntityTypes = new[]
            {
                typeof(IEntity),
                typeof(Entity),
                typeof(AuditableEntity)
            };

            mapper.IsEntity((t, declared) =>
            {
                var isEntity = baseEntityType.IsAssignableFrom(t)
                               && !baseEntityTypes.Contains(t)
                               && !t.ContainsGenericParameters
                               && !t.IsInterface;
                //if (isEntity)
                //{
                //    Logger.Debug("IsEntity --> {0} {1}", declared, t.FullName);
                //}
                return isEntity;
            });
            mapper.IsRootEntity((t, declared) =>
            {
                var isRootEntity = baseEntityTypes.Contains(t.BaseType) && !baseEntityTypes.Contains(t);
                //if (isRootEntity)
                //{
                //    Logger.Debug("IsRootEntity --> {0} {1}", declared, t.FullName);
                //}
                return isRootEntity;
            });

            mapper.BeforeMapProperty += (insp, prop, map) =>
            {
                var type = prop.LocalMember.GetPropertyOrFieldType();
                if (type.IsEnum)
                {
                    var userType = typeof(EnumStringType<>).MakeGenericType(type);
                    map.Type((IType) Activator.CreateInstance(userType));
                }
            };

            mapper.BeforeMapManyToOne += (insp, prop, map) =>
            {
                map.Column(prop.LocalMember.Name + "Id");
                map.Cascade(Cascade.Persist);
                map.ForeignKey("FK_" + prop.GetContainerEntity(insp).Name + "_" + prop.LocalMember.Name);
                map.Index("IX_" + prop.GetContainerEntity(insp).Name + "_" + prop.LocalMember.Name);
            };
            mapper.BeforeMapList += (insp, prop, map) =>
            {
                map.Cascade(Cascade.All);
                map.Table(prop.GetContainerEntity(insp).Name + "_" + prop.LocalMember.Name);
                map.Index(m => m.Column("Idx"));
                map.Key(km => km.Column(prop.GetContainerEntity(insp).Name + "Id"));
            };
            mapper.BeforeMapBag += (insp, prop, map) =>
            {
                map.Cascade(Cascade.All);
                map.Table(prop.GetContainerEntity(insp).Name + "_" + prop.LocalMember.Name);
                map.Key(km => km.Column(prop.GetContainerEntity(insp).Name + "Id"));
            };
            mapper.BeforeMapSet += (insp, prop, map) =>
            {
                map.Cascade(Cascade.All);
                map.Table(prop.GetContainerEntity(insp).Name + "_" + prop.LocalMember.Name);
                map.Key(km => km.Column(prop.GetContainerEntity(insp).Name + "Id"));
            };
            mapper.BeforeMapMap += (insp, prop, map) =>
            {
                map.Cascade(Cascade.All);
                map.Table(prop.GetContainerEntity(insp).Name + "_" + prop.LocalMember.Name);
                map.Key(km => km.Column(prop.GetContainerEntity(insp).Name + "Id"));
            };
            mapper.BeforeMapIdBag += (insp, prop, map) =>
            {
                map.Cascade(Cascade.All);
                map.Table(prop.GetContainerEntity(insp).Name + "_" + prop.LocalMember.Name);
                map.Key(km => km.Column(prop.GetContainerEntity(insp).Name + "Id"));
                map.Id(m => m.Column("Id"));
            };

            mapper.Class<Entity>(
                                 map =>
                                 {
                                     map.DynamicUpdate(true);
                                     map.DynamicInsert(true);
                                     map.Id(x => x.Id,
                                            m => m.Generator(Generators.HighLow,
                                                             h => h.Params(new
                                                             {
                                                                 max_lo = MaxLo
                                                             }))
                                           );
                                     map.Version(x => x.Version, m => m.Generated(VersionGeneration.Never));
                                 });

            CustomizeMappings(mapper);

            // customizations start

            var customizedMappings = assemblies
                                     .SelectMany(x => x.GetExportedTypes())
                                     .Where(x => !x.IsAbstract)
                                     .Where(t => IsGenericSubclassOf(t, typeof(ClassMapping<>))
                                                 || IsGenericSubclassOf(t, typeof(SubclassMapping<>))
                                                 || IsGenericSubclassOf(t, typeof(JoinedSubclassMapping<>))
                                                 || IsGenericSubclassOf(t, typeof(UnionSubclassMapping<>)))
                                     .ToArray();
            //Logger.Debug("Applying mapping customizations: " + string.Join(", ", customizedMappings.Select(x => x.Name).OrderBy(x => x)));
            mapper.AddMappings(customizedMappings);

            // customizations end

            //Logger.Debug("Compiling mappings for types: " + string.Join(", ", types.Select(x => x.Name).OrderBy(x => x)));
            HbmMapping mapping;
            try
            {
                mapping = mapper.CompileMappingFor(types);
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException(
                                                "This exception happened in the past when forgot to map a new property as JSON-serialized and its type is not supported by NH",
                                                ex);
            }

            var mappingFixers = assemblies
                                .SelectMany(x => x.GetExportedTypes())
                                .Where(x => !x.IsAbstract)
                                .Where(t => typeof(IMappingFixer).IsAssignableFrom(t))
                                .ToArray();
            foreach (var mappingFixer in mappingFixers)
            {
                var fixer = (IMappingFixer) Activator.CreateInstance(mappingFixer);
                fixer.Fix(mapping);
            }

            try
            {
                File.WriteAllText("core.hbm.xml", mapping.AsString());
            }
            catch (Exception)
            {
                // this is just for development/debugging info...
                // when deployed, most probably there will not be 
                // permissions to write anything, so just ignore errors here
            }

            configuration.AddDeserializedMapping(mapping, null);
        }

        protected virtual void CustomizeMappings(ConventionModelMapper mapper)
        {
        }

        private bool IsGenericSubclassOf(Type t, Type baseType)
        {
            if (baseType.IsGenericType == false)
                throw new ArgumentException("should be a generic type", "baseType");

            if (baseType.IsGenericTypeDefinition == false)
                throw new ArgumentException("should be an \"open\" generic type", "baseType");

            var typeToCheck = t;
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == baseType)
                    return true;
                typeToCheck = typeToCheck.BaseType;
            }

            return false;
        }
    }
}