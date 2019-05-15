const app = new Vue({

    el: '#app',
    data: {
        titulo: 'Altas Bajas y Modificacion de residencias',
        residencias: [],
        resi: ''
    },
    methods:{  //aqui adentro creamos todos los metodos
        agregarResidencia: function(){
            this.residencias.push({
                nombre: this.resi,
                estado: false
            });
            console.log(residencias);
            this.resi = '';
        },
        editarResidencia: function(unaResidencia){
            this.residencias[residencia].estado = true;

        },
        eliminarResidencia: function(unaResidencia){
            this.residencias.splice(unaResidencia, 1);  
        }

    }

});


  