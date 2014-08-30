namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface IResult<out T>
    { 
        T Value { get; }        
    }
} 
