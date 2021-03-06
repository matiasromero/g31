import Vue from 'vue'
import Router from 'vue-router'
import Index from '@/components/Index'
import SignIn from '@/components/SignIn'
import Login from '@/components/Login'
import Home from '@/components/Home'
import CRUDResidences from '@/components/CRUDResidences'
import AddResidence from '@/components/AddResidence'
import EditResidence from '@/components/EditResidence'
import CRUDAuctions from '@/components/CRUDAuctions'
import AddAuction from '@/components/AddAuction'
import EditAuction from '@/components/EditAuction'
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
      path: '/administrar-residencias',
      name: CRUDResidences,
      component: CRUDResidences
    },
    {
      path: '/agregar-residencia',
      name: AddResidence,
      component: AddResidence
    },
    {
      path: '/editar-residencia/:id',
      name: 'edit-residence',
      component: EditResidence
    },
    {
      path: '/administrar-subastas',
      name: CRUDAuctions,
      component: CRUDAuctions
    },
    {
      path: '/agregar-subasta',
      name: AddAuction,
      component: AddAuction
    },
    {
      path: '/editar-subasta',
      name: EditAuction,
      component: EditAuction
    }
  ]
})
