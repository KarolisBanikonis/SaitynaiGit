<template>
<Header />
    <ModalWindow v-show="isModalVisible" @close="closeModal">
        <template v-slot:body>
            <h2>Neteisingi duomenys</h2>
        </template>
    </ModalWindow>
    
    <h1>Prisijungimas</h1>
    <div class="register">
        <input type="text" v-model="userName" placeholder="Įveskite vartotojo vardą" />
        <input type="password" v-model="password" placeholder="Įveskite slaptažodį" />
        <button v-on:click="login" >Prisijungti</button>
    </div>
    <p class="footer">InfoMovies</p>
</template>

<script>
import ModalWindow from "./ModalWindow";
import axios from 'axios'
import jwt_decode from "jwt-decode"
import Header from './Header.vue'
export default {
    name:'Login',
    data()
    {
        return {
            userName:'',
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
        async login()
        {
            if(this.userName == ''){
                alert("Įveskite vartotojo vardą");
            }
            else if(this.password == ''){
                alert("Įveskite slaptažodį");
            }
            else{
                let request = await axios.post("https://infomoviesapi.azurewebsites.net/api/login",{
                    userName:this.userName,
                    password:this.password
                }).catch(error => {
                    console.log(error);
                    this.isModalVisible = true;
                });
                if (request.status == 200){
                    let jwtToken = JSON.stringify(request.data)
                    let accessToken = JSON.parse(jwtToken).accessToken;
                    localStorage.setItem("token", accessToken)
                    var decodedToken = jwt_decode(accessToken);
                    if(JSON.stringify(decodedToken).includes("SimpleUser")){
                        localStorage.setItem("userRole", "SimpleUser")
                    }
                    if(JSON.stringify(decodedToken).includes("Admin")){
                        localStorage.setItem("userRole", "Admin")
                    }
                    this.$router.push({name:'Home'})
                }
            }
        }
    },
    mounted()
    {
        let user= localStorage.getItem("token");
        if (user){
            this.$router.push({name:'Home'})
        }
    }
}
</script>
