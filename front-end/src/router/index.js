import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import SignIn from '@/components/SignIn'
import Login from '@/components/Login'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/registrarse',
      component: SignIn
    },
    {
      path: '/iniciar-sesion',
      component: Login
    },

  ]
})
