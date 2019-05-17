<template>

<div id="start">

    <div id="hotSales" class="col-lg-11">
        <hot-sale></hot-sale>
        <hot-sale></hot-sale>
        <hot-sale></hot-sale>
        <hot-sale></hot-sale>
        <hot-sale></hot-sale>
    </div>
    <br>
    <div id="residences" class="col-lg-12">
        <residence></residence> <residence></residence> <residence></residence> <residence></residence> <residence></residence>

    </div>


</div>

</template>

<script>

import Residence from '@/components/Residence.vue'
import HotSale from '@/components/hotSale.vue'


export default{
    name: 'Home',
    data() {
        return {
            post: []
        }
    },
    components: {
        Residence,
        HotSale
    },


}

</script>

<style>

    #hotSales{
        background-color: cornflowerblue;
        padding: 70px;
        margin-bottom: 50px;
    }

    #residences{
       
        padding: 70px;
    }

</style>
