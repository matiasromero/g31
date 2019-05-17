<template>

<div>

	<div class="container">

		<div class="card" id="card">

			<div class="card-header" id="card-header"><i class="fa fa-fw fa-globe"></i> <strong>Buscador de Residencias</strong> 
    
        <a href="#/agregar-residencia" class="float-right btn btn-dark btn-sm"><i class="fa fa-fw fa-plus-circle"></i> Agregar Residencia</a>
     
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

							<div class="col-sm-2">

								<div class="form-group">

									<label>Descripcion</label>

									<input type="text" class="form-control" name="description" id="description"  value="" placeholder="Ingrese Descripcion">

								</div>

							</div>

              	<div class="col-sm-2">

								<div class="form-group">

									<label>Caracteristicas</label>

									<input type="text" class="form-control" name="features" id="features"  value="" placeholder="Ingrese Caracteristicas">

								</div>

							</div>

							<div class="col-sm-3">

								<div class="form-group">

									<label>&nbsp;</label>

									<div>

										<button type="submit" name="submit" value="search" id="submit" class="btn btn-primary"><i class="fa fa-fw fa-search"></i> Buscar</button>

										<a href="" class="btn btn-danger"><i class="fa fa-fw fa-sync"></i> Recargar</a>

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

						<th>Nombre</th>

						<th>Ubicacion</th>

						<th>Descripcion</th>

						<th>Caracteristicas</th>

						<th class="text-center">Acciones</th>

					</tr>

				</thead>

				<tbody>

	
					<tr>

						<td></td>

						<td></td>

						<td></td>

						<td></td>

						<td align="center">

							<a href="/#/editar-residencia" class="text-primary"><i class="fa fa-fw fa-edit"></i> Editar</a> | 

							<a href="" class="text-danger" onClick="return confirm('Esta Seguro que desea eliminar esta residencia?');"><i class="fa fa-fw fa-trash"></i> Borrar</a>

						</td>

					</tr>
          

				</tbody>

			</table>

		</div> <!--/.col-sm-12-->

	</div>

</div>

</template>

<style>

#card-header{
  background-color: gainsboro;
  padding: 3px;
}



</style>


