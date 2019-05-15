<template>
  <div class="container mt-5" id="app">
      <abmResidencia>
      <h3>Residencias</h3>
      <h5>{{titulo}}</h5>
      </abmResidencia>
      <input type="text" class="form-control my-3" v-model="resi" v-on:keyup.enter="agregarResidencia">
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
