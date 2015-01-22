using System;

namespace ExtDirect.Direct
{
    public enum AppliedTo
    {        
        DirectPublic,
        DirectWithinAssembly
    }
    public enum MethodVisibility
    {
        Visible,
        Hidden    
    }
    public enum DirectAction
    {
        
        Null,
        Load,
        //Create,
        Update,
        //Delete,
        //Save,
        FormSubmission,
        Store,
        //FreeStyle,
        TreeStore
        
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DirectServiceAttribute : Attribute
    {
        private readonly string _name;
        private readonly AppliedTo _visibility;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public AppliedTo Visibility
        {
            get {
                return _visibility;
            }
        }
        public DirectServiceAttribute()
        {
            _visibility = AppliedTo.DirectPublic;
        }
        public DirectServiceAttribute(string className)
        {
            _name = className;
            _visibility = AppliedTo.DirectPublic;

        }
        public DirectServiceAttribute(string className, AppliedTo visibility)
        {
            _name = className;
            _visibility = visibility;

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
        private readonly MethodVisibility _visibility;
        public MethodVisibility Visibility
        {
            get { return _visibility; }
        }
        public DirectMethodAttribute()
        {
            _visibility = MethodVisibility.Visible;
            _action = DirectAction.Null;
        }
        public DirectMethodAttribute(string name)
        {
            _name = name;
            _visibility = MethodVisibility.Visible;
            _action = DirectAction.Null;

        }
        public DirectMethodAttribute(string name, DirectAction action)
        {
            _name = name;
            _visibility = MethodVisibility.Visible;
            _action = action;

        }
        public DirectMethodAttribute(string name, DirectAction action,MethodVisibility visibility)
        {
            _name = name;
            _visibility = visibility;
            _action = action;

        }
        public DirectAction getDirectAction() {
            return _action;
        }
    }    
}
