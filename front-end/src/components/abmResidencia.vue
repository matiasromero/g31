<template>
  <div class="container mt-5" id="residencia">

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
    name: 'residencia',
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

<style>

  #content{
	margin-top: 0 auto;
	border:1px solid #eee;
	border-radius: 2px;
	background: white;
	box-shadow: 0px 2px, 5px #CCC;
	padding: 0px;
}

#header{
	background: #00A1E1;
	color:white;
	margin:0px;
	height: 90px;
	line-height: 90px;
}

#header h1{
	display: block;
	line-height: 50px;
	font-weight: bold;
	font-size: 30px;
}

#nav{
	padding: 0px;
	margin: 0px;
	background: #4C4D4F;

}

#nav ul{
	list-style: none;
	margin: 0px;
	padding-left: 20px;
}

#nav ul li{
	float: right;
	margin-right: 20px;

}

#nav ul li a{
	display: block;
	color: white;
	height: 30px;
	line-height: 29px;
	padding-left: 10px;
	padding-right: 10px;
	transition: all 300ms;


}

#nav ul li a:hover{
	text-decoration: none;
	background: rgb(103, 104, 107);
}

#main {
	padding: 35px;
  margin-top: 10px;
  margin-bottom: 100px;
	border-radius: 5px;
}

#footer{
	margin-top: 30px;
	padding-top: 10px;
	border:1px solid #eee;
	color: rgb(92, 92, 92);

}

#footer p{
	text-align: center;
}

  .router-link-exact-active{
    text-decoration: none !important;
	  background: rgb(103, 104, 107);
  }

</style>

