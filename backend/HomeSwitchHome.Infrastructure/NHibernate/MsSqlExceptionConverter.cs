using System;
using System.Data.SqlClient;
using NHibernate.Exceptions;

namespace HomeSwitchHome.Infrastructure.NHibernate
{
    public class MsSqlExceptionConverter : ISQLExceptionConverter
    {
        public Exception Convert(AdoExceptionContextInfo exInfo)
        {
            var sqle = ADOExceptionHelper.ExtractDbException(exInfo.SqlException) as SqlException;
            if (sqle != null)
                switch (sqle.Number)
                {
                    case 2601: // Violation in unique index
                    case 2627: // Violation in unique constraint
                        return new ConstraintViolationException(exInfo.Message, sqle, exInfo.Sql, null);

                    case 547: // constraint violation (referential integrity)
                    {
                        if (exInfo.Message.StartsWith("could not delete"))
                            return new CannotDeleteException(exInfo.Message, sqle);

                        return new ConstraintViolationException(exInfo.Message, sqle, exInfo.Sql, null);
                    }
                }
            return SQLStateConverter.HandledNonSpecificException(exInfo.SqlException,
                                                                 exInfo.Message, exInfo.Sql);
        }
    }
}