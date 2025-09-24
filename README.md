# It-s-spheres-raining

## Overview
This Unity project implements a **falling sphere spawner** system with fading, glowing, collisions, and nudging mechanics.  
The system creates spheres that fall from a fixed spawn point, fade in during the fall, interact with the ground, collide with each other, and eventually fade out and get destroyed.

---

## How it works

### Spawning
- **Spawner.cs** controls the creation of spheres at a fixed position.
- Spawn rate is adjustable and limited by parameter.
- Each sphere is instantiated with random scale for variation.

### Falling and Fading
- **Fader.cs** manages the transparency:
  - Sphere starts fully transparent.
  - While falling, its alpha increases.
  - On landing, sphere becomes fully opaque.
  - On collision with another landed sphere, or after some time, it fades out and gets destroyed.

### Nudging
- **Nudger.cs** applies a small random impulse when a sphere first lands, causing it to roll slightly.
- **ExternalNudger.cs** provides area-based forces (attraction or repulsion) that affect rigidbodies within a radius. This can be toggled to behave like a **repulsor** or a **black hole**.

### Collision
- **Sphere.cs** tracks landing events via UnityEvents and sets the `IsLanded` flag.
- When two landed spheres collide, both trigger fade-out.

### Glow Effect
- **Glower.cs** adds a glowing emissive effect:
  - Emission color oscillates between blue and black over time.
  - Each sphere has a random phase offset so their glow pulses asynchronously.
  - The glow intensity is scaled by the sphere’s alpha — transparent spheres glow less.
