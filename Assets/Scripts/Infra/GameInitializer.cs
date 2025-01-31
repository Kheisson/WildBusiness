using System.Collections.Generic;
using System.IO;
using Infra.Injector;
using UnityEngine;

namespace Infra
{
    public class GameInitializer : SingletonMono<GameInitializer>
    {
        private readonly List<IInjector> _injectors = new List<IInjector>();

        public void RegisterInjector(IInjector injector, InjectionType injectionType)
        {
            _injectors.Add(injector);

            if (injectionType == InjectionType.Instantly)
            {
                injector.Inject();
            }
        }

        private void Awake()
        {
            foreach (var injector in _injectors)
            {
                if (injector.InjectionTiming == InjectionType.Awake)
                {
                    injector.Inject();
                }
            }
        }

        private void Start()
        {
            foreach (var injector in _injectors)
            {
                if (injector.InjectionTiming == InjectionType.Start)
                {
                    injector.Inject();
                }
            }
        }

        private void OnEnable()
        {
            foreach (var injector in _injectors)
            {
                if (injector.InjectionTiming == InjectionType.OnEnable)
                {
                    injector.Inject();
                }
            }
        }
#if UNITY_WEBGL
        //TODO: Remove this method
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R)) return;
            
            var path = Application.persistentDataPath;
                
            var files = Directory.GetFiles(path, "*.sav");

            foreach (var file in files)
            {
                File.Delete(file);
                Debug.Log($"Deleted save file: {file}");
            }
        }
#endif
    }
}