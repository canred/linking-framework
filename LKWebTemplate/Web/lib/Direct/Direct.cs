using System;

namespace ExtDirect.Direct
{
    //public enum AppliedTo
    //{        
    //    DirectPublic,
    //    DirectWithinAssembly
    //}
    
    public enum DirectAction
    {        
        Null,
        Load,
        //Update,
        FormSubmission,
        Store,
        TreeStore        
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DirectServiceAttribute : Attribute
    {
        private readonly string _name;
        //private readonly AppliedTo _visibility;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        //public AppliedTo Visibility
        //{
        //    get {
        //        return _visibility;
        //    }
        //}
        //public DirectServiceAttribute()
        //{
        //    _visibility = AppliedTo.DirectPublic;
        //}
        //public DirectServiceAttribute(string className)
        //{
        //    _name = className;
        //    _visibility = AppliedTo.DirectPublic;

        //}
        public DirectServiceAttribute(string className/*, AppliedTo visibility*/)
        {
            _name = className;
            //_visibility = visibility;
        }
    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class DirectMethodAttribute : Attribute
    {
        private readonly string _name;
        
        public string MethodName
        {
            get { return _name; }
        }
        private readonly DirectAction _action;
        public DirectAction Action
        {
            get { return _action; }
        }
        
        //public DirectMethodAttribute()
        //{        
        //    _action = DirectAction.Null;
        //}
        //public DirectMethodAttribute(string name)
        //{
        //    _name = name;         
        //    _action = DirectAction.Null;

        //}
        public DirectMethodAttribute(string name, DirectAction action)
        {
            _name = name;
            _action = action;
        }
        
        public DirectAction getDirectAction() {
            return _action;
        }
    }    
}
