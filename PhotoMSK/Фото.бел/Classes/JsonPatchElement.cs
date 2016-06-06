using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotobel.Classes
{
    public class JsonPatchElement
    {
        public string Op { get; set; } // "add", "remove", "replace", "move", "copy" or "test"
        public string Path { get; set; }
        public string Value { get; set; }
    }
}