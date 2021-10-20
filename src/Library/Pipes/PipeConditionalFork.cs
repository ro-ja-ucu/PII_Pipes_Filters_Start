using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompAndDel;


namespace CompAndDel.Pipes
{
    public class PipeConditionalFork : IPipe
    {
        protected IPipe nextPipeTrue;
        protected IPipe nextPipeFalse;
        protected IFilterConditional filtro;
        
        public PipeConditionalFork(IPipe nextPipeTrue, IPipe nextPipeFalse, IFilterConditional filtro) 
        {
            this.nextPipeTrue = nextPipeTrue;
            this.nextPipeFalse = nextPipeFalse;           
            this.filtro = filtro;
        }
        
        public IPicture Send(IPicture picture)
        {
            IPicture result = filtro.Filter(picture);
            bool conditionResult = filtro.ConditionResult;

            if (conditionResult)
            {
                return this.nextPipeTrue.Send(picture);
            }
            else
            {
                return this.nextPipeFalse.Send(picture);
            }
        }
    }
}
