struct CameraUniform {
    view_proj: mat4x4<f32>
}

@group(0) @binding(0) 
var<uniform> camera: CameraUniform;

struct VertexInput {
    @builtin(vertex_index) index: u32,
    @location(0) model_col0: vec4<f32>,
    @location(1) model_col1: vec4<f32>,
    @location(2) model_col2: vec4<f32>,
    @location(3) model_col3: vec4<f32>,
    @location(4) color: vec4<f32>
}

struct VertexOutput {
    @builtin(position) position: vec4<f32>,
    @location(0) uv: vec2<f32>,
    @location(1) color: vec4<f32>
}

@group(1) @binding(0)
var texture: texture_2d<f32>;
@group(1) @binding(1)
var tex_sampler: sampler;

@vertex
fn vs_main(in: VertexInput) -> VertexOutput {
    var out: VertexOutput;

    let vertex_position = vec3<f32>(
        f32(in.index & 0x1u),
        f32((in.index & 0x2u) >> 1u),
        0.0
    );

    out.position = camera.view_proj * mat4x4<f32>(
        in.model_col0,
        in.model_col1,
        in.model_col2,
        in.model_col3
    ) * vec4<f32>(vertex_position, 1.0);
    out.uv = vec2<f32>(vertex_position.xy);
    out.color = in.color;
    return out;
}

@fragment
fn fs_main(in: VertexOutput) -> @location(0) vec4<f32> {
    return textureSample(texture, tex_sampler, in.uv) * in.color;
}