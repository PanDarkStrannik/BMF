﻿
namespace CharacterStateMechanic
{
    public interface IStateConnection
    {
        bool IsConnectionReady
        { get; }
        bool IsConnectionBanned
        { get; }

        void ChangeBanValue(IStateConnection reasonForChange);
    }
}