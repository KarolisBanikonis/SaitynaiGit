<template>
<Header />
    
    <h1>Filmus kuriančių kompanijų sąrašas</h1>
    <div class="register">
        <button v-if="showIfAdmin()" v-on:click="createCompany()">
            <img class="img" src="../assets/add.svg">
            Sukurkite kompaniją
            </button>
    </div>
    <br>
    <br>
    <ModalWindow v-show="isModalVisible" @close="closeModal">
        <template v-slot:body>
            <h2>Kompanija</h2>
            <li>Id - {{this.company.id}}</li>
            <li>Pavadinimas - {{this.company.name}}</li>
            <li>Aprašymas - {{this.company.description}}</li>
        </template>
    </ModalWindow>
    <table>
        <td class="firstRow">Pavadinimas</td>
        <td class="firstRow">Aprašymas</td>
        <td class="firstRow"></td>
        <td class="firstRow" v-if="showIfAdmin()"></td>
        <td  class="firstRow" v-if="showIfAdmin()"></td>
        <tr v-for="company in companies" :key="company.id">
            <td class="highlight" v-on:click="getCompany(company)">{{company.name}}</td>
            <td class="highlight" v-on:click="getCompany(company)">{{company.description}}</td>
            <td class="highlight" v-on:click="getMovies(company.id)">Filmai</td>
            <td class="highlight" v-if="showIfAdmin()" v-on:click="updateCompany(company.id)">Atnaujinti</td>
            <td class="highlight" v-if="showIfAdmin()" v-on:click="deleteCompany(company.id)">Ištrinti</td>
        </tr>   
    </table>
    <p class="footer">InfoMovies</p>
</template>
<script>

import axios from 'axios'
import Header from './Header.vue'
import ModalWindow from "./ModalWindow";
export default{
    name:'Companies',
    data(){
        return {
            companies:[],
            token:'',
            isModalVisible: false,
            company:[]
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
        showIfAdmin(){
            let role = localStorage.getItem("userRole");
            if(role == "Admin"){
                return true;
            }
            return false;
        },
        createCompany(){
            this.$router.push({name: 'CreateCompany'});
        },
        updateCompany(id){
            localStorage.setItem("chosenCompany", id);
            this.$router.push({name: 'UpdateCompany'});
        },
        async deleteCompany(id){
            await axios.delete("https://infomoviesapi.azurewebsites.net/api/companies/"+id, { headers: {"Authorization" : `Bearer ${this.token}`} });
            location.reload();
        },
        getCompany(company){
            this.company = company;
            this.isModalVisible = true;
        },
        async getMovies(id){
            let request =await axios.get("https://infomoviesapi.azurewebsites.net/api/companies/"+id);
            localStorage.setItem("chosenCompany", JSON.stringify(request.data));
            this.$router.push({name: 'Movies'}); 
        }
    },
    async mounted(){
        this.token = localStorage.getItem("token");
        let request = await axios.get("https://infomoviesapi.azurewebsites.net/api/companies/");
        this.companies = request.data;
    }
}
</script>
