# MURMUR — hitos 3 y 4

Escena: `Assets/Scenes/SampleScene.unity`. Unity 6000.3.13f1, URP/VFX Graph 17.3.0, Input System. Se conserva WebGPU como unica API de Web.

## Interaccion

`MURMUR Toroidal Ring` conserva su toro de 200 x 20 muestras. La simulacion ahora vive en `Assets/Effects/MurmurIron.vfx`, con un burst de 4000 fragmentos y sin envejecimiento ni emisiones repetidas. C# no recorre las particulas.

`MurmurToroidalRing` proyecta el cursor sobre un plano paralelo a la camara que pasa por el centro del anillo. Al pulsar captura punto y radio en coordenadas locales; estos permanecen constantes hasta soltar. La influencia GPU es `1 - smoothstep(0, radioCapturado, distance(reposo, puntoInicial))`. Fuera del radio es cero. El objetivo es reposo mas desplazamiento ponderado; se mantiene mientras se pulsa.

El atributo persistente `targetPosition` guarda la posicion de reposo (no se sobrescribe). El bloque de Update resuelve el muelle criticamente amortiguado de forma analitica: omega = sqrt(rigidez), decaimiento = exp(-omega * dt). La integracion automatica de posicion esta desactivada para no integrar dos veces. Soltar, perder foco, pausar o desactivar el componente elimina el desplazamiento objetivo. Arrastre limitado a 6 unidades y bounds de 24 x 24 x 24.

Parametros en el Inspector:
- Influence Radius: 1.65 unidades locales, capturado al pulsar.
- Return Stiffness: 64; mayor valor implica retorno mas rapido.
- Roughness: 0.32; se envia Smoothness = 1 - Roughness a la salida URP Lit Mesh.

## Presentacion

Pedestal claro, fondo neutro, camara fija a tres cuartos, luz principal rasante y relleno suave. Fragmentos opacos alargados de malla, metallic=1. El entorno de reflexion esta guardado en `Assets/Materials/MurmurStudioReflection.asset` y asignado a un Reflection Probe Custom. No necesita HDRI externo ni configurar nodos manualmente.

El efecto multicolor del hito 1 se conserva desactivado para dejar visible el gesto. La etiqueta de API permanece activa. No se han agregado controles del hito 5 ni postprocesados.

## Comprobaciones

Play Mode: 4000 particulas GPU; fotogramas avanzan. Arrastre mediante dispositivo Mouse temporal del Input System; cinta sostenida con boton pulsado. Comparacion de capturas: lado opuesto sin diferencias; imagen tras soltar practicamente identica al reposo. Al simular OnApplicationFocus(false): IsDragging=false y GrabActive=0. Rugosidad 0.05 frente a 0.9 modifica el reflejo con la misma iluminacion. Las pruebas temporales no se incluyen en Assets.

La build se guarda en `build/`. Servir por localhost, no abrir index.html mediante file://. Para repetir visualmente: pulsar un borde, arrastrar, mantener 2 segundos, soltar; repetir fuera del anillo y cambiando de ventana. Comprobar etiqueta `Active API: WebGPU`.
