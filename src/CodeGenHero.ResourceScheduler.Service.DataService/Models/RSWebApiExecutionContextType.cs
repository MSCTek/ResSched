using CodeGenHero.DataService;
using System;
using static CodeGenHero.ResourceScheduler.Service.DataService.Constants.Enums;

namespace CodeGenHero.ResourceScheduler.Service.DataService.Models
{
    public class RSWebApiExecutionContextType : WebApiExecutionContextType
    {
        public override int Current
        {
            get
            {
                return _current;
            }
            set
            {
                switch (value)
                {
                    case (int)ExecutionContextTypes.Base:
                        _current = value;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"The value provided, {value}, for the current WebApiExecutionContextType is invalid.");
                }
            }
        }
    }
}