using System;

namespace ISU.Lesson.UNITYEvent
{
    public interface IInteractable
    {
        void OnInteract();

        void OnExitInteraction();

        Action OnExitInteractionEvent { get; set; }
    }
}