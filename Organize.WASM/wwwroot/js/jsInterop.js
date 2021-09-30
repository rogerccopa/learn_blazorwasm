window.addEventListener('resize', () => {
    // Call the static method OnResize in the Organize assembly
    console.log("Static Resize from js");
    DotNet.invokeMethodAsync("Organize.WASM", "OnResize");
})