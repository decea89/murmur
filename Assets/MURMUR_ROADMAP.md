# MURMUR · Roadmap para una tarde en Unity
**Entrega:** una build WebGPU de escritorio con un anillo de hierro manipulable, tres controles y un clip de 12–15 segundos capturado en el navegador. Presupuesto orientativo de cuatro horas desde que Unity y el módulo Web estén instalados. La publicación en hosting es opcional.

## Antes de empezar
Crea un proyecto Universal 3D en Unity 6.6 con Web Build Support, ábrelo en el Editor y abre la misma carpeta raíz en Codex. Si ya tienes proyecto, parte de su versión y configuración y comprueba compatibilidad antes de cambiarla. Ten a mano un navegador de escritorio con WebGPU habilitado. Usa una sola escena, cámara fija y ratón. Guarda este archivo en la raíz del proyecto.

## 0:00–0:35 · Ver una partícula en la web
**Tú:** abre Build Profiles, activa Web y añade la escena. En Player > Web > Other Settings desactiva Auto Graphics API y selecciona WebGPU como única API para esta primera prueba. Instala Visual Effect Graph desde Package Manager si falta. Añade un efecto básico visible y haz Build And Run.
**Prompt para Codex:** «Inspecciona la versión del Editor, URP, VFX Graph y el sistema de entrada. Prepara un indicador de la API gráfica activa con SystemInfo.graphicsDeviceType. Dame los pasos de Editor pendientes con los nombres de mi versión. De momento solo quiero verificar un efecto visible en la build WebGPU».
**Prueba:** abre la build servida en localhost, comprueba que indica WebGPU y que ves el efecto. Si el lienzo sale vacío, prueba un cubo URP junto a las partículas. Si aparece el cubo, revisa el efecto, su material y sus bounds. Si tampoco aparece, revisa consola del navegador y configuración de la build. Reserva el acabado hasta superar esta prueba.

## 0:35–1:15 · Construir el anillo gris
**Tú:** crea un VFX Graph con un único burst y una salida de malla sencilla. Trabaja con unas 4.000 partículas iniciales y cámara a tres cuartos. Busca una silueta con hueco legible.
**Prompt para Codex:** «Guíame bloque a bloque para distribuir limaduras sobre la superficie de un toro en VFX Graph. Guarda la posición inicial de cada partícula en un atributo personalizado restPosition, en espacio local. Usa partículas persistentes, sin emisiones repetidas. Dame la lista de atributos y parámetros y una prueba de reposo. Para los cambios de grafo que necesiten Editor, indícame qué conectar».
**Prueba:** al esperar 30 segundos el anillo conserva densidad, posición y silueta. Deja margen en los bounds para la futura cinta estirada.

## 1:15–2:10 · Agarrar, mantener y soltar
**Tú:** define un plano de arrastre paralelo a la cámara que pase por el centro del anillo. Ajusta el radio inicial para levantar un borde. Haz otra build al terminar el gesto.
**Prompt para Codex:** «Implementa el controlador de arrastre con el sistema de input existente. Proyecta el puntero sobre el plano y transforma inicio y destino al espacio local del VFX. Al pulsar, usa la distancia de cada restPosition al punto inicial para fijar su influencia, con caída suave. Mientras arrastro, desplaza el objetivo de esas partículas por el delta del puntero ponderado. Usa un muelle amortiguado con delta de tiempo para seguir el objetivo. Al soltar o perder el foco, devuelve el objetivo al reposo. Expón radio y rigidez. Entrega el C# y las conexiones precisas del grafo. Evita trabajo por partícula en C#».
**Prueba:** arrastra desde un borde, mantén dos segundos y suelta. La cinta sigue suspendida mientras mantienes pulsado, el lado opuesto conserva la forma y el anillo se recompone sin vibración persistente. Repite diez veces, incluida una pulsación fuera del anillo. Compruébalo en navegador.

## 2:10–2:50 · Darle peso al metal
**Tú:** monta un pedestal claro y fondo neutro. Usa fragmentos opacos y alargados, material metálico, un entorno de reflexión sencillo y una luz lateral amplia en apariencia. Ajusta primero el encuadre y el reflejo en reposo; después observa cómo viaja el brillo al tirar de una cinta.
**Prompt para Codex:** «Con la salida iluminada compatible con mi URP/VFX, expón la rugosidad. Si la entrada del shader es Smoothness, convierte con smoothness = 1 - roughness. Indica cómo comprobar ambos extremos bajo la misma iluminación y cómo conectar el parámetro al efecto».
**Prueba:** en una captura pequeña se distingue el borde iluminado del anillo, el hueco y la cinta. Cambiar la rugosidad modifica la anchura del reflejo sin mover la luz.

## 2:50–3:20 · Convertirlo en una herramienta
**Tú:** coloca un panel compacto a un lado. Deja el centro libre y comprueba el tamaño del texto en la ventana de captura.
**Prompt para Codex:** «Con la UI que ya use el proyecto, conecta tres sliders al efecto: radio del imán, rigidez de retorno y rugosidad. Añade restablecer valores y una tecla para ocultar el panel. Impide iniciar un arrastre de la escultura al tocar la UI. Muestra la API activa y el tiempo medio por fotograma con Time.unscaledDeltaTime sobre una ventana de un segundo».
**Prueba:** radio mínimo y máximo producen cintas claramente distintas con el mismo gesto; la rigidez cambia la velocidad de vuelta; la rugosidad cambia el reflejo. Reset recupera el aspecto inicial. Los sliders funcionan en la build.

## 3:20–4:00 · Medir, grabar y entregar
**Tú:** con la build final abierta, anota navegador, GPU, resolución de render y cantidad configurada en el burst. Tras el calentamiento, prueba 30 segundos de arrastre a esa resolución. Usa como objetivo orientativo 60 FPS (16,7 ms por fotograma). La medición con delta de tiempo representa el fotograma completo, no el coste aislado de GPU. Si el movimiento pierde fluidez, baja la cantidad de partículas o la resolución y vuelve a medir.
**Prompt para Codex:** «Revisa la escena de la build, parámetros por defecto, reinicio y pérdida de foco. Dime qué validar manualmente en navegador. Con los datos que yo mida, redacta una ficha breve para portfolio; deja vacíos los valores sin medir».
**Clip:** 0–3 s, arranca un hilo fino. 3–6 s, mantenlo y suelta. 6–9 s, sube el radio en el panel. 9–15 s, repite el gesto desde el mismo borde y deja que se cierre el anillo. Guarda también una captura de la cinta suspendida.
**Entrega mínima:** carpeta de build probada con Build And Run, clip del navegador y ficha de medición. Si sobra tiempo, publica en un host HTTPS y abre el enlace desde una sesión nueva antes de compartirlo. Si vas justo, conserva el gesto, radio, acabado y clip; aplaza los controles restantes y el hosting.

## Documentación de Unity
[WebGPU en Unity 6.6](https://discussions.unity.com/t/webgpu-out-of-experimental-in-unity-6-6/1734694) · [Activar WebGPU](https://docs.unity.com/en-us/engine/6000.6/manual/platform-specific/webgl/develop/graphics/web-gpu/enable)
