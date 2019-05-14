using AutoMapper;
using HomeSwitchHome.Infrastructure.Domain;

namespace HomeSwitchHome.Infrastructure.Mapping
{
    public static class AutomapperExtensions
    {
        public static IMappingExpression<TSource, TTarget>
            IgnoreAuditProperties<TSource, TTarget>(
                this IMappingExpression<TSource, TTarget> mappingExpression)
            where TTarget : IHaveAuditInformation
        {
            mappingExpression.ForMember(vm => vm.CreatedAt, mo => mo.Ignore());
            mappingExpression.ForMember(vm => vm.CreatedBy, mo => mo.Ignore());
            mappingExpression.ForMember(vm => vm.UpdatedAt, mo => mo.Ignore());
            mappingExpression.ForMember(vm => vm.UpdatedBy, mo => mo.Ignore());

            return mappingExpression;
        }
    }
}