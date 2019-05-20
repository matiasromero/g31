<template>
  <div id="App" class="col-lg-12">

       
      <header id="header" class="col-lg-12">
          <h1>Home SWITCHhome </h1>
      </header>
		
			<nav class="nav  justify-content-end" id="nav" v-if="authenticated == false" >
						<router-link tag="li" router-link-active to="/iniciar-sesion"><a>Iniciar Sesion</a></router-link>   
            <router-link tag="li" router-link-active to="/registrarse"><a>Registrarse </a></router-link>
      </nav>
			<nav class="nav  justify-content-end" id="nav" v-if="authenticated" >
						<router-link tag="li" router-link-active to="/administrar-Residencias"><a>Administrar Residencias</a></router-link>   
						<li class="nav-item">
							<a v-on:click="logout()" tag="li">Salir</a>
						</li>
      </nav>

      <section id="main" class="col-lg-12">
        <!-- Componente actual -->
        <router-view @authenticated="setAuthenticated"></router-view>
      </section>

      <footer id="footer">
          <p>FOOTER</p>
      </footer>

  </div>
</template>
<script>
export default {
	name: 'App',
	data() {
		return {
			authenticated: false
		}
	},
	mounted() {
		if (localStorage.getItem('token'))
			this.authenticated = true;
	},
	methods: {
            setAuthenticated(status) {
							console.log(status);
                this.authenticated = status;
            },
						logout() {
								this.authenticated = false;
								localStorage.removeItem('token');
                localStorage.removeItem('username');
								localStorage.removeItem('userid');
								this.$router.push("/");
            }
        }
}
</script>

<style>

  #content{
	margin-top: 0 auto;
	border:1px solid #eee;
	border-radius: 2px;
	background: white;
	box-shadow: 0px 2px, 5px #CCC;
	padding: 0px;
}

#header{
	background: #00A1E1;
	color:white;
	margin:0px;
	height: 90px;
	line-height: 90px;
}

#header h1{
	display: block;
	line-height: 50px;
	font-weight: bold;
	font-size: 30px;
}

#nav{
	padding: 0px;
	margin: 0px;
	background: #4C4D4F;

}


#nav li{
	margin-left: 10px;
}

#nav  li a{
	display: block;
	color: white;
	height: 30px;
	line-height: 29px;
	padding-left: 10px;
	padding-right: 10px;
	transition: all 300ms;


}

#nav  li a:hover{
	text-decoration: none;
	background: rgb(103, 104, 107);
}

#main {
	padding: 35px;
  margin-top: 10px;
  margin-bottom: 100px;
	border-radius: 5px;
}

#footer{
	margin-top: 30px;
	padding-top: 10px;
	border:1px solid #eee;
	color: rgb(92, 92, 92);

}

#footer p{
	text-align: center;
}

  .router-link-exact-active{
    text-decoration: none !important;
	  background: rgb(103, 104, 107);
  }

</style>
