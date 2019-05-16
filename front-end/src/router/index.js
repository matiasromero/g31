import Vue from 'vue'
import Router from 'vue-router'
import Index from '@/components/Index'
import SignIn from '@/components/SignIn'
import Login from '@/components/Login'
import Home from '@/components/Home'
import ResidenceDetail from '@/components/ResidenceDetail'
import abmResidencia from '@/components/abmResidencia'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Index',
      component: Index
    },
    {
      path: '/registrarse',
      component: SignIn
    },
    {
      path: '/iniciar-sesion',
      component: Login
    },
    {
      path: '/inicio',
      name: Home,
      component: Home
    },
    {
      path: '/residencia:id',
      name: ResidenceDetail,
      component: ResidenceDetail
    },
    {
      path:'/abmResidencia',
      component: abmResidencia
    },

  ]
})
