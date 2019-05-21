<template>

<div id="start">

    <div id="hotSales" class="col-lg-12">
      
    </div>
    <br>
    <div id="residences" >

        <div class="container col-lg-11 ">
            <div class="card "  >
                <div class="card-header"><i class="fa fa-fw fa-hammer"></i> <strong> Subastas</strong>  </div>
                <div class="card-body">
                    <div v-for="post in posts" :key= post.id  id="residence">
                        <div class="row">
                            <div class="col-sm-6">
                                <img v-bind:src="post.thumbnailUrl"   width="150" height="150"/> 
                            </div>
                            <div class="col-sm-2">
                                <form>
                                	<a href="" class="btn btn-danger"><i class="fa fa-fw fa-certificate"></i> Pujar</a>
                                    <br><br>
                                    <input type="number" name="offer" v-model="offer" class="form-control input-sm" placeholder="Monto">
                                </form>
                            </div>
                            <div class="vl"></div>
                             <div class="col-sm-2">
                                	<h4>Valor Actual </h4>
                                    <h3>$999.99</h3>
                                    <input type="hidden" name="currentValue" v-model="currentValue" >
                                    <a href="/detalle-subasta">Detalles</a>
    
                            </div>
                        </div>
                    <hr>
                    
                    </div>
                   
                </div>
            </div>
        </div> 
    </div>
</div>

</template>

<script>

import axios from 'axios'

import Residence from '@/components/Residence.vue'
import HotSale from '@/components/hotSale.vue'


export default{
    name: 'Home',
    data() {
        return {
            posts: []
        }
    },
    components: {
        HotSale
    },
    mounted(){
        let vue = this;
        axios.get('https://jsonplaceholder.typicode.com/photos')
        .then( function( response ) {
            vue.posts = response.data;
            console.log(vue.posts);
        }) 
    
    }
}

</script>

<style>

    #hotSales{
       
    }

    #residences{
       
        padding: 70px;
    }

    .vl {
         border-left: 1px solid grey;
        height: 150px;
        margin-right: 20px;
    }

</style>
