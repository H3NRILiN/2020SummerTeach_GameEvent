namespace ISU.Lesson.UNITYEvent
{
    public interface IInteractable
    {
        CameraAnimationTarget m_CamAnimTarget { get; set; }

        void OnInteract();

        void OnExitInteraction();
    }
}