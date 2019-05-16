<template>

    <div class="container signIn-container">
            <div class="row">
                <div class="col-md-6 signIn-form">
                    <h3>Formulario</h3>
                    <form  v-on:submit="checkForm()" >
                        <div class="form-group">
                            <input type="text" class="form-control" placeholder="Nombre *"  required value="" />
                        </div>
                        <div class="form-group">
                            <input type="text" class="form-control" placeholder="Apellido *" required value="" />
                        </div>
                        <div class="form-group">
                            <input type="email" class="form-control" placeholder="Email *" required value="" />
                        </div>
                        <div class="form-group">
                            <input type="date" class="form-control" placeholder="Fecha Nac *" v-model="fechaNac"  required value="" />
                        </div>
                        <div class="form-group">
                            <input type="password" class="form-control" placeholder="Contraseña *" v-model="contraseña" required value="" />
                        </div>
                        <div class="form-group">
                            <input type="password" class="form-control" placeholder="Confirmar Contraseña *" v-model="confirmar" required value="" />
                        </div>
                        <div class="form-group">
                            <input type="submit" class="btnSubmit" value="Registrar" />
                        </div>
                    </form>
                </div>
            </div>
    </div>
  
</template>

<script>

export default{
    
}


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
 
.signIn-container{
     margin-left: 28%;
  
}

.signIn-form{
    padding: 5%;
    background: #00A1E1;
    box-shadow: 0 5px 8px 0 rgba(0, 0, 0, 0.2), 0 9px 26px 0 rgba(0, 0, 0, 0.19);
}
.signIn-form h3{
    text-align: center;
    color: #fff;
}
.signIn-container form{
    padding: 10%;
}
.btnSubmit
{
    width: 50%;
    border-radius: 1rem;
    padding: 1.5%;
    border: none;
    cursor: pointer;
}

.signIn-form .btnSubmit{
    font-weight: 600;
    color: #00A1E1;
    background-color: #fff;
}
.login-form-2 .ForgetPwd{
    color: #fff;
    font-weight: 600;
    text-decoration: none;
}

</style>
