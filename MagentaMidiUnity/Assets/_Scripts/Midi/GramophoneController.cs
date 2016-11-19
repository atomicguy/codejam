/*
------------------------------------------------------------------
Copyright (c) 2016 VIORAMA UG  
This software is the proprietary information of VIORAMA UG
All rights reserved.
------------------------------------------------------------------
*/
using UnityEngine;
using System.Collections;
using Klak.Midi;
using MidiJack;

namespace Viorama
{
	/// <summary>
	/// MidiListener class does something for some reason
	/// </summary>
    public class GramophoneController : MonoBehaviour 
	{
        public enum NoteFilter {
            Off, NoteName, NoteNumber
        }

        public enum NoteName {
            C, CSharp, D, DSharp, E, F, FSharp, G, GSharp, A, ASharp, B
        }

		#region MEMBER VARIABLES
        [SerializeField]
        MidiChannel _channel = MidiChannel.All;

        [SerializeField]
        NoteFilter _noteFilter = NoteFilter.Off;

        [SerializeField]
        NoteName _noteName;

        [SerializeField]
        int _lowestNote = 60; // C4

        [SerializeField]
        int _highestNote = 60; // C4
        [Header("Visual FX")]
        [SerializeField]
        private Light gramophoneLight;
        [SerializeField]
        private Gradient gradient;
        [SerializeField]
        private ParticleSystem particles;
        [SerializeField]
        private int particlesPerNote = 20;
        [SerializeField]
        private float maxVelocity = 20f;
		#endregion
		
		#region DELEGATES
		#endregion
		
		#region UNITY FUNCTIONS

        void OnEnable()
        {
            MidiMaster.noteOnDelegate += NoteOn;
            MidiMaster.noteOffDelegate += NoteOff;
        }

        void OnDisable()
        {
            MidiMaster.noteOnDelegate -= NoteOn;
            MidiMaster.noteOffDelegate -= NoteOff;
        }
		
		void Start ()
		{
			gramophoneLight.enabled = false;
		}

		
		#endregion
		
		#region METHODS


        bool CompareNoteToName(int number, NoteName name)
        {
            return (number % 12) == (int)name;
        }

        bool FilterNote(MidiChannel channel, int note)
        {
            if (_channel != MidiChannel.All && channel != _channel) return false;
            if (_noteFilter == NoteFilter.Off) return true;
            if (_noteFilter == NoteFilter.NoteName)
                return CompareNoteToName(note, _noteName);
            else // NoteFilter.Number
                return _lowestNote <= note && note <= _highestNote;
        }
		#endregion
		
		#region EVENT HANDLERS
        void NoteOn(MidiChannel channel, int note, float velocity)
        {
            if (!FilterNote(channel, note)) return;

            Debug.Log("Note " + note + " Velocity " + velocity);
            Color noteColor = gradient.Evaluate((float)note/127f);
            gramophoneLight.color = noteColor;

            gramophoneLight.enabled = true;
            particles.startColor = noteColor;
            particles.startSpeed = velocity;
            particles.Emit(particlesPerNote);


            //particles.
//            velocity = _velocityCurve.Evaluate(velocity);

//            _noteOnEvent.Invoke();
//            _noteOnVelocityEvent.Invoke(velocity);
//
//            _floatValue.targetValue = _onValue * velocity;
        }

        void NoteOff(MidiChannel channel, int note)
        {
            gramophoneLight.enabled = false;
//            if (!FilterNote(channel, note)) return;
//
//            _noteOffEvent.Invoke();
//
//            _floatValue.targetValue = _offValue;
        }
		#endregion
		
		#region PROPERTIES
		#endregion
		
	}
}