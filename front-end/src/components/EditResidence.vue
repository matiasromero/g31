<template>

<div class="container ">
		<div class="card" >
			<div class="card-header"><i class="fa fa-fw fa-plus-circle"></i> <strong> Editar Residencia</strong> 
                <a href="#/administrar-residencias" class="float-right btn btn-dark btn-sm"><i class="fa fa-fw fa-home"></i> Residencias</a>
            </div>
			<div class="card-body">
				
				<div class="col-sm-6">
					<h5 class="card-title">Los Campons con <span class="text-danger">*</span> Son Obligatorios!</h5>
					<div v-if="success != ''" class="alert alert-success" role="alert">
                          {{success}}
                    </div>
					<form action="#" @submit.prevent="formSubmit" enctype="multipart/form-data">
						<div class="form-group">
							<label>Nombre <span class="text-danger">*</span></label>
							<input type="text" name="name" v-model="name" id="username" class="form-control" placeholder="Ingrese un nombre"  required>
						</div>
						<div class="form-group">
							<label>Dirección<span class="text-danger">*</span></label>
							<input type="text" name="address" id="address" v-model="address" class="form-control" placeholder="Ingrese una ubicacion"  required>
						</div>
						<div class="form-group">
							<label>Descripcion <span class="text-danger">*</span></label>
							<textarea name="description" id="description" v-model="description" class="form-control" placeholder="Ingrese una descripcion"  required />
						</div>
                        <div class="form-group"> 
							<img :src="imageSrc" width="150" height="150"/> <br>
							<strong>Imagen: *</strong>
                        		<input type="file" class="form-control" v-on:change="onImageChange">
							</div>
						<div class="form-group">
							<button type="submit" name="submit" value="submit" id="submit" class="btn btn-primary"><i class="fa fa-fw fa-plus-circle"></i> Agregar</button>
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>

</template>


<script>
import axios from 'axios';

export default {
	data: function () {
    return {
		name: '',
		description: '',
		address: '',
		file: '',
		imageUrl: '',
		success: ''
    }
  },
  mounted() {
	if (!localStorage.getItem("token"))
		  this.$router.push('/');
	var token = localStorage.getItem("token");
        	const config = {
				headers: { 'Authorization': "bearer " + token }
    		}
            let uri = 'https://localhost:5001/api/v1/residences/' + this.$route.params.id;
            axios.get(uri).then((response) => {
				let residence = response.data;
				this.name = residence.name;
				this.description = residence.description;
				this.address = residence.address;
				console.log('https://localhost:5001/images/' + residence.imageUrl);
				this.imageSrc = 'https://localhost:5001/images/' + residence.imageUrl
            });
  },
  methods: {
	onImageChange(e){
        console.log(e.target.files[0]);
    	this.file = e.target.files[0];
    },
    formSubmit(e) {
		var token = localStorage.getItem("token");
    	let currentObj = this;
        const config = {
			headers: { 
				'content-type': 'multipart/form-data',
			'Authorization': "bearer " + token }
    	}
 
		let formData = new FormData();
		formData.append('name', this.name);
		formData.append('description', this.description);
		formData.append('address', this.address);
        formData.append('file', this.file);
        axios.put('https://localhost:5001/api/v1/residences/' + this.$route.params.id, formData, config)
        	 .then((response) => {
				 this.$router.push('/administrar-residencias')
        	})
            .catch(function (error) {
            	currentObj.output = error;
			});
		}
	}
}
</script>
