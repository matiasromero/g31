<template>
  <div id="App" class="col-lg-12">

       <nav id="nav" class="col-lg-12">
          <ul>
            <router-link tag="li" router-link-active to="/registrarse"><a>Registrarse </a></router-link>
            <router-link tag="li" router-link-active to="/iniciar-sesion"><a>Iniciar Sesion</a></router-link>    
            <router-link tag="li" router-link-active to="/abmResidencia"><a>Residencias</a></router-link>
					</ul>
        </nav>
      <header id="header" class="col-lg-12">
          <h1>Home SWITCHhome </h1>
      </header>

      <section id="main" class="col-lg-12">
        <!-- Componente actual -->
        <router-view></router-view>
      </section>

      <footer id="footer">
          <p>FOOTER</p>
      </footer>

  </div>
</template>
<script>
export default {
  name: 'App'
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

#nav ul{
	list-style: none;
	margin: 0px;
	padding-left: 20px;
}

#nav ul li{
	float: right;
	margin-right: 20px;

}

#nav ul li a{
	display: block;
	color: white;
	height: 30px;
	line-height: 29px;
	padding-left: 10px;
	padding-right: 10px;
	transition: all 300ms;


}

#nav ul li a:hover{
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
