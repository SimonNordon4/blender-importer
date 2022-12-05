namespace BlenderImporter.Events
{
    public class EventManager
    {
        // maybe these can't be static, or the delegates can but the individual events can't
        public delegate void BlenderProcessEvent(bool success);
        public static BlenderProcessEvent OnBlenderProcessFinished;
        
        public delegate void FBXEvent(string guid);
        public static FBXEvent OnFBXPreProcess;
        public static FBXEvent OnFBXImported;
    }
}