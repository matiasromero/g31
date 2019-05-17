<template>
    
<div class="container">
		<div class="card">
			<div class="card-header"><i class="fa fa-fw fa-edit"></i> <strong>Editar Residencia</strong> 
            <a href="#/administrar-residencias" class="float-right btn btn-dark btn-sm"><i class="fa fa-fw fa-home"></i> Residencias</a>
            </div>
			<div class="card-body">
				<div class="col-sm-6">
					<h5 class="card-title">Los Campons con <span class="text-danger">*</span> Son Obligatorios!</h5>
					<form method="post">
						<div class="form-group">
							<label>Nombre <span class="text-danger">*</span></label>
							<input type="text" name="username" id="username" class="form-control" placeholder="Ingrese un nombre"  required>
						</div>
						<div class="form-group">
							<label>Ubicacion<span class="text-danger">*</span></label>
							<input type="email" name="useremail" id="useremail" class="form-control" placeholder="Ingrese una ubicacion"  required>
						</div>
						<div class="form-group">
							<label>Descripcion <span class="text-danger">*</span></label>
							<input type="tel" name="userphone" id="userphone" class="form-control" placeholder="Ingrese una descripcion"  required>
						</div>
                        <div class="form-group">
							<label>Caracteristicas <span class="text-danger">*</span></label>
							<input type="tel" name="userphone" id="userphone" class="form-control" placeholder="Ingrese Caracteristicas"  required>
						</div>
						<div class="form-group">
                        <button type="submit" name="submit" value="submit" id="submit" class="btn btn-primary"><i class="fa fa-fw fa-edit"></i> Actualizar</button>
						</div>
					</form>
				</div>
			</div>
		</div>
</div>

</template>
