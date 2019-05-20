<template>

<div>

	<div class="container">
		<div class="card" id="card">
			<div class="card-header" id="card-header"><i class="fa fa-fw fa-globe"></i> <strong>Buscador de Residencias</strong> 
		<router-link tag="a" router-link-active to="/agregar-residencia" class="float-right btn btn-dark btn-sm"><a>Agregar Residencia</a></router-link>
     </div>
     
      <br>
    	<div class="card-body">
				<div class="col-sm-12">
					<h5 class="card-title"><i class="fa fa-fw fa-search"></i>Encontrar Residencia</h5>
					<form method="get">
						<div class="row">
							<div class="col-sm-2">
								<div class="form-group">
									<label>Nombre Residencia</label>
									<input type="text" name="residence" id="residence" class="form-control" value="" placeholder="Ingrese nombre">
								</div>
							</div>

							<div class="col-sm-2">
								<div class="form-group">
									<label>Ubicacion</label>
									<input type="text" name="location" id="location" class="form-control" value="" placeholder="Ingrese Ubicacion">
								</div>
							</div>


							<div class="col-sm-3">
								<div class="form-group">
									<label>&nbsp;</label>
									<div>
										<button type="submit" name="submit" value="search" id="submit" class="btn btn-primary"><i class="fa fa-fw fa-search"></i> Buscar</button>
										<a href="#" v-on:click="fetchItems()"  class="btn btn-danger"><i class="fa fa-fw fa-sync"></i> Recargar</a>
									</div>
								</div>
							</div>
						</div>
					</form>
				</div>
			</div>
		</div>

  <br>
	
		<div>

			<table class="table table-striped table-bordered">
				<thead>
					<tr class="bg-primary text-white">
						<th></th>
						<th>Nombre</th>
						<th>Ubicacion</th>
						<th>Descripcion</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					<tr v-for="item in residences" :key="item.id">
                    <td align="center"><i>#{{item.id}}</i></td>
                    <td>{{ item.name }}</td>
                    <td>{{ item.address }}</td>
					<td>{{ item.description }}</td>
                    <td>
						<router-link :to="{name: 'edit-residence', params: { id: item.id }}" class="text-primary"><i class="fa fa-fw fa-edit"></i></router-link> |
						<a href="#" v-on:click="deleteResidence(item)" class="text-danger"><i class="fa fa-fw fa-trash"></i></a>
					</td>
                </tr>
					<tr>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
					</tr>
				</tbody>
			</table>
		</div> <!--/.col-sm-12-->
	</div>
</div>
</template>

<script>
import Axios from 'axios';

export default {
	data: function () {
    	return {
			residences: []
    	}
  	},
   	created: function() {
        this.fetchItems();
    },
  	mounted() {
		if (!localStorage.getItem("token"))
      		this.$router.push('/');
  	},
  	methods: {
		deleteResidence(item) {
			var r = confirm("Desea eliminar la residencia?");
			if (r == true) {
				var token = localStorage.getItem("token");
				const config = {
						headers: { 
								'Authorization': "bearer " + token 
							}
						}
						Axios.delete('https://localhost:5001/api/v1/residences/' + item.id, config)
							.then((response) => {
								this.fetchItems();
							})
							.catch(function (error) {
								currentObj.output = error;
							});
			} 
		  	},
		fetchItems() {
			var token = localStorage.getItem("token");
        	const config = {
				headers: { 'Authorization': "bearer " + token }
    		}
            let uri = 'https://localhost:5001/api/v1/residences';
            Axios.get(uri).then((response) => {
                this.residences = response.data;
            });
        }
	}
}
</script>

<style>

#card-header{
  background-color: gainsboro;
  padding: 3px;
}



</style>


