# MURMUR

An interactive sculpture of 4,000 iron fragments, exploring how far real-time graphics and GPU simulation can go in a web browser with **WebGPU**.

The project brings together a sculptural visual style, metallic surfaces and tactile interaction. Grab and deform the sculpture, change its silhouette, set it in motion, or let it collapse and watch the fragments rebuild themselves. Rendering and simulation run in real time; the experience is not a prerendered video.

## Features

- Persistent particles simulated on the GPU with Unity VFX Graph and custom HLSL.
- Mouse interaction with a smooth influence falloff and damped spring return.
- Ring, pentagon, star and waving-hand silhouettes, with animated transitions.
- Optional wave motion, gravity-driven collapse, floor/pedestal collisions, bounce, friction and progressive reconstruction.
- Metallic PBR shading, HDR environment reflections, animated lighting, ACES tonemapping and SMAA.
- A fixed **3840 × 2160** render canvas, scaled to fit the browser window.
- Controls for influence radius, return stiffness and roughness, plus an active graphics API and FPS indicator.

## Open the project

1. Install **Unity 6.3 LTS — 6000.3.13f1**, including **Web Build Support**.
2. Clone this repository and open its root folder through Unity Hub. Let Unity restore the packages.
3. Open `Assets/Scenes/SampleScene.unity`.

The project uses **URP 17.3.0**, **VFX Graph 17.3.0** and **Input System 1.19.0**. Package versions and rendering settings are included in the repository.

## Controls

| Input | Action |
| --- | --- |
| Left mouse button + drag | Grab and deform a region of the sculpture |
| Release mouse button | Return the fragments smoothly to rest |
| Shape buttons | Change the silhouette |
| Space | Toggle animated motion |
| R | Collapse / reconstruct |
| H | Show / hide the control panel |
| P | Show / hide the entire interface |
| Sliders / Reset | Adjust interaction and roughness / restore defaults |

## Run in a browser

In **File → Build Profiles**, select the **Web** platform and keep `SampleScene` as the enabled scene. The project includes the MURMUR web template and WebGPU configuration. Choose **Build And Run** and an output directory such as `build/`.

Use a browser and GPU that support WebGPU. If Unity cannot launch the browser, select the default browser in its Web preferences, or configure the executable path for the selected browser. Serve an exported build over localhost or HTTPS rather than opening `index.html` directly. A production host must serve Unity's compressed build files with the appropriate content types and compression headers.

The 4K canvas is intentional: frame rate depends on the GPU, browser and display workload. The on-screen FPS counter reports the running experience; this repository does not promise a fixed frame rate on every device.

## Project layout

- `Assets/Scenes/SampleScene.unity` — sculpture and studio presentation.
- `Assets/Effects/MurmurIron.vfx` — GPU simulation and rendering.
- `Assets/Scripts/` — interaction, shape controls, presentation and UI.
- `Assets/WebGLTemplates/Murmur/` — 4K browser canvas and loading screen.

Built and iterated with **Codex and the official Unity plugin for Codex**, exploring a conversational workflow for scene authoring, VFX, materials and interaction.

Generated builds, recordings and Unity's local caches are not included.
