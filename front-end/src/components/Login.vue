<template>
  <div class="container login-container">
            <div class="row">
                <div class="col-md-6 login-form-2">
                    <h3>Iniciar Sesion</h3>
                    <form>
                        <div class="form-group">
                            <input type="text" class="form-control" placeholder="Email *" required value="" />
                        </div>
                        <div class="form-group">
                            <input type="password" class="form-control" placeholder="Contraseña *" required value="" />
                        </div>
                        <div class="form-group">
                            <input type="submit" class="btnSubmit" value="Ingresar" />
                        </div>
                        <div class="form-group">

                            <a href="#" class="ForgetPwd" value="Login">Olvidaste tu contraseña?</a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
</template>

<script>

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.login-container{
     margin-left: 28%;
  
}

.login-form-2{
    padding: 5%;
    background: #00A1E1;
    box-shadow: 0 5px 8px 0 rgba(0, 0, 0, 0.2), 0 9px 26px 0 rgba(0, 0, 0, 0.19);
}
.login-form-2 h3{
    text-align: center;
    color: #fff;
}
.login-container form{
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

.login-form-2 .btnSubmit{
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