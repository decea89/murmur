# MURMUR — Movimiento GPU y salida 4K

Objetivo: al menos 30 FPS (33,3 ms por fotograma). Por petición del usuario se omite la comparación de rendimiento en esta iteración; ese objetivo queda sin verificar en navegador.

Las 4.000 partículas se animan en VFX Graph mediante ondas radiales y axiales y rotación de las facetas. C# envía tres parámetros por fotograma, sin recorrer las partículas. Se conservan posiciones de reposo e interacción con muelle amortiguado. La fase se congela durante el agarre y continúa al soltar.

Valores de escena en MurmurMotion: amplitud 0,38, velocidad 0,65, torsión 32 grados. Espacio activa/desactiva el movimiento con transición suave; H muestra/oculta el panel.

## Configuración

- Unity 6000.3.13f1; URP/VFX 17.3.0; sistema de entrada existente.
- WebGPU como única API web. Build sin Development Build ni conexión al Profiler.
- Canvas fijo de 3840 × 2160; matchWebGLToCanvasSize=false y devicePixelRatio=1. Presentación CSS ajustada a la ventana conservando 16:9. Incluso una ventana pequeña solicita un canvas 4K.
- Calidad PC por defecto en Web, renderScale=1, HDR, SMAA High y texturas a resolución completa. Sin supermuestreo adicional por encima de 4K.
- Se conservan sombras suaves de 2048, entorno HDR y postprocesado de estudio. Renderer diferido; sin MSAA añadido.
- RuntimeSpeed, IL2CPP OptimizeSpeed, Brotli, excepciones explícitas. Memoria inicial WebAssembly 128 MB, máxima 2048 MB.
- Sin paquetes nuevos ni cambio de versión de Unity.

La animación se revisó en Play Mode con 4.000 partículas y sin errores de consola. La resolución descrita corresponde a la configuración de salida; no se ha medido el rendimiento de esta build 4K. El indicador existente muestra tiempo medio de fotograma sobre una ventana de un segundo, no tiempo GPU.

El problema previo de textos intermitentes de uGUI en capturas WebGPU sigue pendiente; esta iteración no lo da por resuelto.

Restauración de assets anteriores: C:/Users/Fernando de Cea/Murmur-before-motion.
