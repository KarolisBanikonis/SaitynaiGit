<template>
    <!-- <p>Current width is: {{ windowWidth }}</p> -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <!-- Top Navigation Menu -->
    <div class="topnav">
        <div class ="nav">
            <div class ="nav" v-if="showBurgerMenu()">
                <router-link to="/">InfoMovies</router-link>
                <!--<a href="javascript:void(0);" class="icon" v-on:click="toggleBurgerMenu()">
                    <i class="fa fa-bars"></i>
                </a>-->
            </div>
            <div class ="nav" id="webapp_cover" v-if="showBurgerMenu()">
                <div id="menu_button">
                    <input type="checkbox" id="menu_checkbox">
                    <label for="menu_checkbox" id="menu_label" v-on:click="toggleBurgerMenu()">
                        <div id="menu_text_bar" ></div>
                    </label>
                </div>
            </div>
            <div class ="nav" v-if="!showBurgerMenu()">
                <router-link class="fontHeader" to="/">InfoMovies</router-link>
                <router-link v-show="loggedOut()" to="/login">Prisijungti</router-link>
                <router-link v-show="loggedOut()" to="/register">Registruotis</router-link>
                <a v-show="loggedIn()" v-on:click="Logout()">Atsijungti</a>
            </div>
        </div>
        <!-- Burgeris -->
        <!--<div id="burgerSlideOut" v-if="showBurgerMenu()">
            <router-link v-show="loggedOut()" to="/login">Prisijungti</router-link>
            <router-link v-show="loggedOut()" to="/register">Registruotis</router-link>
            <a v-show="loggedIn()" v-on:click="Logout()">Atsijungti</a>
        </div> -->
        <div id="burgerSlideOut" v-if="showBurgerMenu()">
            <router-link v-show="loggedOut()" to="/login">Prisijungti</router-link>
            <router-link v-show="loggedOut()" to="/register">Registruotis</router-link>
            <a v-show="loggedIn()" v-on:click="Logout()">Atsijungti</a>
        </div>
    </div>
</template>
<script>
export default {
    name:'Header',
    data(){
        return {
            isMenuVisible: true,
            windowWidth: window.innerWidth
        }
    },
    methods:{
        showBurgerMenu(){
            if(this.windowWidth <= 700){
                return true;
            }
            else{
                return false;
            }
        },
        toggleBurgerMenu(){
            var x = document.getElementById("burgerSlideOut");
            if (x.style.display === "block") {
                x.style.display = "none";
                this.isMenuVisible = true;
            } else {
                x.style.display = "block";
                this.isMenuVisible = false;
            }
        },
        loggedInWithMenu(){
            if (localStorage.getItem("token") && this.isMenuVisible){
                return true;
            }
            return false;
        },
        loggedOutWithMenu(){
            if (localStorage.getItem("token")){
                return false;
            }
            return true;
        },
        Logout(){
            localStorage.clear();
            this.$router.push({name:'Home'});
            location.reload();
        },
        loggedIn(){
            if (localStorage.getItem("token")){
                return true;
            }
            return false;
        },
        loggedOut(){
            if (localStorage.getItem("token")){
                return false;
            }
            return true;
        }
    },
    mounted() {
        window.onresize = () => {
            this.windowWidth = window.innerWidth
        }
    }
}
</script>