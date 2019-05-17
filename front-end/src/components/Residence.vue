<template>

    <router-link tag="div"  router-link-active to="/residencia:id" class="row" ><a></a>
    <div v-for="post in posts" v-bind:key="post.id"  class="col-sm"  id="residence">


     <img v-bind:src="post.thumbnailUrl"   width="150" height="150"/> 
    
    </div>
    
   

    </router-link>


  
</template>

<script>

import axios from 'axios'

export default {

   data() {
        return {
            posts: []
        }
    },
    mounted(){
        let vue = this;
        axios.get('https://jsonplaceholder.typicode.com/photos')
        .then( function( response ) {
            vue. posts = response.data;
            console.log(vue.posts);
        }) 
    
    }

}


</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

 #residence {
    padding: 10px;
    display: inline-block;
    margin-bottom: 20px;
    margin-right: 10px;
   
  
}


</style>