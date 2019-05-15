const app = new Vue({

    el: '#app',
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

});


  