<template>
    <div class="container">
		<div class="card" id="card">
			<div class="card-header" id="card-header"><i class="fa fa-fw fa-hammer"></i> <strong>Subastas</strong> 
		<router-link tag="a" router-link-active to="/agregar-subasta" class="float-right btn btn-dark btn-sm"><a>Agregar Subasta</a></router-link>
     </div>
     
      <br>
    	<div class="card-body">
				<div class="col-sm-12">
					<h5 class="card-title"><i class="fa fa-fw fa-search"></i>Buscar Subasta </h5>
					<form method="get">
						<div class="row">
							<div class="col-sm-2">
								<div class="form-group">
									<label>Nombre Subasta</label>
									<input type="text" name="residence" id="residence" class="form-control" value="" placeholder="Ingrese nombre">
								</div>
							</div>

							<div class="col-sm-2">
								<div class="form-group">
									<label>Fecha:</label>
									<input type="date" name="location" id="location" class="form-control" value="" placeholder="Ingrese Ubicacion">
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
						<th>Fecha Inicio</th>
						<th>Fecha Fin</th>
						<th>Monto Inicial</th>
						<th>Acciones</th>
					</tr>
				</thead>
				<tbody>
					<tr v-for="item in auctions" :key="item.id">
                    <td align="center"><i>#{{item.id}}</i></td>
                    <td>{{ item.startDate }}</td>
                    <td>{{ item.endDate }}</td>
					<td>{{ item.startingAmount }}</td>
                    <td>
                        <a href="#" v-on:click="closeAuction(item)" class="text-dark"><i class="fa fa-fw fa-ok"></i>Cerrar Subasta</a>
						<router-link :to="{name: 'edit-auction', params: { id: item.id }}" class="text-primary"><i class="fa fa-fw fa-edit"></i></router-link> |
						<a href="#" v-on:click="deleteAuction(item)" class="text-danger"><i class="fa fa-fw fa-trash"></i></a>
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


</template>

<script>

import Axios from 'axios';

export default {
	data: function () {
    	return {
		auctions: []
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
	deleteAuction(item) {
		var r = confirm("Desea eliminar esta subasta?");
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
