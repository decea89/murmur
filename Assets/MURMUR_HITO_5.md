# MURMUR — Hito 5

La escena SampleScene conserva la escultura VFX, los materiales y la configuración WebGPU.

## Controles

- Radio del imán: 1–2,7; inicial 1,65. El radio se captura al iniciar cada agarre.
- Rigidez de retorno: 16–144; inicial 64. Muelle críticamente amortiguado en GPU.
- Rugosidad: 0–1; inicial 0,32. Conecta con Smoothness = 1 - rugosidad del VFX URP Lit.
- Restablecer valores recupera los valores de la escena al iniciar y libera el agarre.
- H oculta o muestra el panel. El indicador de API y tiempo de fotograma permanece visible.

El panel usa uGUI y el Input System existente. Un raycast contra la UI al pulsar impide iniciar el agarre desde un control, incluso si el cursor sale del panel mientras sigue pulsado.

El indicador consulta SystemInfo.graphicsDeviceType. El tiempo de fotograma se estima sumando Time.unscaledDeltaTime y dividiendo por el número de fotogramas, actualizando tras acumular al menos un segundo. Es tiempo medio de fotograma, no tiempo de GPU ni un benchmark. No hay cifras prefijadas.

## Editor y ejecución

No requiere conexiones manuales de referencias, materiales ni nodos. Abrir Assets/Scenes/SampleScene.unity y pulsar Play. Para reconstruir: File > Build Profiles, perfil Web existente, Build And Run, carpeta build en la raíz del proyecto. No abrir index.html directamente: usar el servidor HTTP de Build And Run o http://localhost:8765/ mientras esté activo el servidor local.

## Comprobación

Play Mode: extremos de los tres sliders conectados, reset a 1,65 / 64 / 0,32, ocultar y mostrar, y bloqueo del agarre al pulsar UI comprobados. Build final completada sin errores. Las advertencias de shaders de paquetes existentes no se han ocultado.

Las capturas y registros de las pruebas de navegador se guardan fuera de Assets, en C:/Users/Fernando de Cea/murmur-work/.
