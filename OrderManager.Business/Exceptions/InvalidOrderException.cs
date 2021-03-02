﻿using System;
using System.Runtime.Serialization;

namespace OrderManager.Business.Exceptions
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException() { }
        public InvalidOrderException(string message) : base(message) { }
        public InvalidOrderException(string message, Exception inner) : base(message, inner) { }
        protected InvalidOrderException(
         SerializationInfo info,
         StreamingContext context) : base(info, context) { }
    }
}
