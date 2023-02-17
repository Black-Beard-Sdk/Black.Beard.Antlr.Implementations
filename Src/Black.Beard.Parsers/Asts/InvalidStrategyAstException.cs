namespace Bb.Asts
{
    [Serializable]
    public class InvalidStrategyAstException : Exception
    {
        public InvalidStrategyAstException()
        {
        
        }
        
        public InvalidStrategyAstException(string object1, string object2, string rule) 
            : base($"object initialize with {object1}. {object2} can't be used." + rule) 
        {
        
        }
        
        public InvalidStrategyAstException(string message, Exception inner) 
            : base(message, inner) 
        {
        
        }

        protected InvalidStrategyAstException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) 
            : base(info, context) 
        {
        
        }
    }



}
