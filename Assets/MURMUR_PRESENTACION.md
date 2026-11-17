# MURMUR — Acabado de estudio para presentación

Dirección: escultura de hierro en estudio oscuro. Grafito y azul frío en sombras; reflejos de softbox cálidos; interfaz en marfil y latón apagado. El gesto de agarrar y soltar sigue siendo el protagonista.

## Cambios

- Misma escena, VFX Graph, 4.000 partículas persistentes, controles e Input System.
- Fragmento de hierro con sección facetada, pequeñas variaciones deterministas de tamaño y orientación. Se corrigieron los ángulos de inicialización: VFX espera grados. Se escalonó y desplazó ligeramente el muestreo del toro para evitar hileras mecánicas. Cada posición de reposo sigue guardada en GPU; el muelle y el agarre no se modificaron.
- Pedestal de basalto biselado con textura procedural de grano mineral y base retranqueada. Materiales URP Lit, sin dependencias externas.
- Luces de estudio cálida, fría y cenital; entorno de reflexión HDR de 256 px por cara. Fondo oscuro con transición atmosférica suave.
- Perfil de postprocesado dedicado: ACES, contraste 12, saturación -8, exposición +0,2; bloom 0,12, umbral 1,3; viñeta 0,22. No se usa desenfoque de movimiento ni profundidad de campo.
- SMAA High y escala interna 1,25 en el perfil PC existente. Web pasa de la calidad Mobile (escala 0,8) a PC para que la versión de escritorio conserve el acabado. Esto aumenta el coste de renderizado; el tiempo de fotograma se mide en vivo, no se presenta como un benchmark universal.
- Plantilla web propia que ocupa la ventana y conserva la resolución del navegador; densidad de píxeles limitada a 1,5. El tamaño de presentación ya no queda fijado en 960 × 600. La configuración WebGPU no cambia.
- Firma discreta y tipografía del panel ajustada. H conserva su función.

## Restauración

Antes de editar se copiaron escenas, efectos, materiales, ajustes y ProjectSettings en `C:/Users/Fernando de Cea/Murmur-before-editorial`. No se han mezclado ni descartado los cambios previos del usuario en Git. No se instalaron paquetes.

## Revisión visual

Valoración subjetiva de la presentación, no métrica de rendimiento:

- Paleta: 3/5 → 4/5 — separación fría/cálida coherente entre metal, base e interfaz.
- Estilo: 2/5 → 4/5 — pedestal biselado y facetas sustituyen el aspecto de primitivas sin acabado.
- Iluminación: 2/5 → 4/5 — reflejos y contornos legibles, sombras con profundidad.
- Silueta: 3/5 → 4/5 — toro reconocible y fragmentos siguiendo su curva.
- Interfaz: 3/5 → 3/5 — compacta y funcional; se puede ocultar para la captura.
- Respuesta al gesto: 4/5 → 4/5 — agarre y muelle GPU conservados.

Referencias consultadas: skills `craft-game-floors` (scene-lighting y visual-scorecard), `urp-postprocessing` (effect-reference) y `unity-cli` del plugin oficial. Documentación de [postprocesado URP en Unity 6.3](https://docs.unity.com/en-us/engine/6000.3/manual/post-processing-and-full-screen-effects/urp/in-urp), [Bloom](https://docs.unity.com/en-us/engine/6000.3/manual/post-processing-and-full-screen-effects/urp/effect-list/bloom) y código de Unity 6000.3 para la selección de calidad por plataforma.

Las capturas del Editor son Direct3D12. Solo las capturas del navegador con la etiqueta API: WebGPU son evidencia de la build web.
