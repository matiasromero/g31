<template>
  <div id="app" class="col-lg-12">

      <header id="header" class="col-lg-12">
          <h1>Home SWITCHhome </h1>
      </header>

      <nav id="nav" class="col-lg-12">
          <ul>
            <router-link tag="li" router-link-active to="/"><a>Home</a></router-link>
            <router-link tag="li" router-link-active to="/acerca-de"><a>Acerca de... </a></router-link>
          </ul>
      </nav>

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
</style>
