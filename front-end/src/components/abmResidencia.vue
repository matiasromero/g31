<template>
  <div class="container mt-5" id="App">

      <h3>Residencias</h3>
      <h5>{{titulo}}</h5>
      
      <input type="text" class="form-control my-3" v-model="nombre" v-on:keyup.enter="agregarResidencia" placeholder="Nombre">
      <input type="text" class="form-control my-3" v-model="nombre" v-on:keyup.enter="agregarResidencia" placeholder="direccion">
      <input type="number" class="form-control my-3" v-model="nombre" v-on:keyup.enter="agregarResidencia" placeholder="cantidad habitaciones">
      <button class="btn btn-primary" @click="agregarResidencia">Agregar</button>

  <div class="mt-3" >
  
  <div class="alert alert-primary" role="alert">
      <div class="d-flex justify-content-between align-items-center">
        <div>
          sdfsdf
        </div>
        <div>
          <button class="btn btn-success btn-sm" @click="editarResidencia">Editar</button>
          <button class="btn btn-danger btn-sm" @click="eliminarResidencia">Eliminar</button>
        </div> 
      </div>
  </div>    
  </div>     
  </div>
  
</template>

<script>
  export default {
    name: 'App',
    data: {
      
        titulo: 'Altas Bajas y Modificacion de residencias',
        residencias: [],
        nombre: '',
        direccion: '',
        habitaciones: ''
     
    },
    methods:{  //aqui adentro creamos todos los metodos
        agregarResidencia: function(){
            this.residencias.push({
                nombre: this.nombre,
                direccion: this.direccion,
                habitaciones: this.habitaciones
            });
            console.log(residencias);
            this.nombre = '';
            this.direccion = '';
            this.habitaciones = '';
        },
        editarResidencia: function(unaResidencia){
            this.residencias[residencia].estado = true;

        },
        eliminarResidencia: function(unaResidencia){
            this.residencias.splice(unaResidencia, 1);  
        }

    }

  };
</script>

