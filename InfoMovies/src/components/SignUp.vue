<template>
<Header />
    <h1>Registracija</h1>
    <div class="register">
        <input type="text" v-model="userName" placeholder="Įveskite vartotojo vardą" />
        <input type="text" v-model="email" placeholder="Įveskite elektroninį paštą" />
        <input type="password" v-model="password" placeholder="Įveskite slaptažodį" />
        <button v-on:click="register" >Registruotis</button>
    </div>

    <ModalWindow v-show="isModalVisible" @close="closeModal">
        <template v-slot:body>
            <h2>Neteisingi duomenys</h2>
        </template>
    </ModalWindow>
    <p class="footer">InfoMovies</p>
</template>
<script>

import ModalWindow from "./ModalWindow";
import axios from 'axios'
import Header from './Header.vue'
axios.defaults.headers.get['header-name'] = 'value'
export default{
    name: 'SignUp',
    data()
    {
        return {
            userName:'',
            email:'',
            password:'',
            isModalVisible: false,
        }
    },
    components:{
        Header,
        ModalWindow
    },
    methods:{
        closeModal() {
            this.isModalVisible = false;
        },
        async register()
        {
            if(this.userName == ''){
                alert("Įveskite vartotojo vardą");
            }
            else if(this.email == ''){
                alert("Įveskite elektroninį paštą");
            }
            else if(this.password == ''){
                alert("Įveskite slaptažodį");
            }
            else{
                let request = await axios.post("https://infomoviesapi.azurewebsites.net/api/register",{
                    userName:this.userName,
                    email:this.email,
                    password:this.password
                }).catch(error => {
                    console.log(error);
                    this.isModalVisible = true;
                });
                if (request.status == 201){
                    this.$router.push({name:'Home'})
                }
            }
        }
    }
}
</script>