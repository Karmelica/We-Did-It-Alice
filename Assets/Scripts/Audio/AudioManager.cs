using System;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        private List<EventInstance> _instancesList = new();
        private EventInstance _starHoverInstance;
        
        public EventInstance levelSelectAmbientInstance;
        public EventInstance menuAmbientInstance;
        public EventInstance levelAmbient;
        
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            _starHoverInstance = CreateNewEventInstance(FMODEvents.Instance.starHover);
            levelAmbient = CreateNewEventInstance(FMODEvents.Instance.levelAmbient);
            menuAmbientInstance = CreateNewEventInstance(FMODEvents.Instance.menuAmbient);
            levelSelectAmbientInstance = CreateNewEventInstance(FMODEvents.Instance.levelSelectAmbient);
        }

        public void Purr()
        {
            PlaySoundOneShot(FMODEvents.Instance.catPurr, transform.position);
        }

        private void Start()
        {
            menuAmbientInstance.start();
        }

        public void ButtonClick()
        {
            PlaySoundOneShot(FMODEvents.Instance.uiButtonClick, transform.position);
        }
        
        public void StarClick()
        {
            PlaySoundOneShot(FMODEvents.Instance.starClick, transform.position);
        }

        public void StarHover()
        {
            _starHoverInstance.start();
        }

        public void StarStopHover()
        {
            _starHoverInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        private EventInstance CreateNewEventInstance(EventReference eventReference)
        {
            var instance = RuntimeManager.CreateInstance(eventReference);
            _instancesList.Add(instance);
            return instance;
        }
        
        public void PlaySoundOneShot(EventReference eventReference, Vector3 soundPos)
        {
            RuntimeManager.PlayOneShot(eventReference, soundPos);
        }

        public void CleanUp()
        {
            foreach(var instance in _instancesList)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
            }
        }
    }
