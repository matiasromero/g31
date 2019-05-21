<template>

<div class="container ">
		<div class="card" >
			<div class="card-header"><i class="fa fa-fw fa-plus-circle"></i> <strong> Agregar Subasta</strong> 
                <a href="#/administrar-subastas" class="float-right btn btn-dark btn-sm"><i class="fa fa-fw fa-hammer"></i> Subastas</a>
            </div>
			<div class="card-body">
				
				<div class="col-sm-6">
					<h5 class="card-title">Los Campons con <span class="text-danger">*</span> Son Obligatorios!</h5>
					<div v-if="success != ''" class="alert alert-success" role="alert">
                          {{success}}
                    </div>
					<form action="#" @submit.prevent="formSubmit" enctype="multipart/form-data">
						<div class="form-group">
							<label>Residencia <span class="text-danger">*</span></label>
							<input type="text" name="residence" v-model="residence" id="residence" class="form-control" placeholder="Ingrese id residencia"  required>
						</div>
						<div class="form-group">
							<label>Fecha Inicio<span class="text-danger">*</span></label>
							<input type="date" name="startDate" id="startDate" v-model="startDate" class="form-control"  required>
						</div>
						<div class="form-group">
							<label>Fecha Fin <span class="text-danger">*</span></label>
							<input type="date" name="endDate" id="endDate" v-model="endDate" class="form-control"   required />
						</div>
                        <div class="form-group">
							<label>Monto Inicial *</label>
                        		<input type="number" name="startingAmount" id="startingAmount" v-model="startingAmount" class="form-control" placeholder="Ingrese un monto" required>
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
		residence: '',
		startDate: '',
		endDate: '',
		startingAmount: '',
		success: ''
    }
  },
  mounted() {
  if (!localStorage.getItem("token"))
    this.$router.push('/');
  },
   methods: {
    formSubmit(e) {
		var token = localStorage.getItem("token");
    	let currentObj = this;
        const config = {
			headers: { 
				'content-type': 'multipart/form-data',
			'Authorization': "bearer " + token }
    	}
 
		let formData = new FormData();
		formData.append('residence', this.residence);
		formData.append('startDate', this.startDate);
		formData.append('endDate', this.endDate);
        formData.append('startingAmount', this.startingAmount);
        axios.post('https://localhost:5001/api/v1/residences', formData, config)
        	 .then((response) => {
				 this.$router.push('/administrar-subastas')
        	})
            .catch(function (error) {
            	currentObj.output = error;
			});
		}
	}


}

</script>