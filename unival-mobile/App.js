import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import Login from './Pages/Login/Login';
import { NavigationContainer } from '@react-navigation/native'
import { createStackNavigator } from '@react-navigation/stack'

import Toast, { ErrorToast } from 'react-native-toast-message';
import Home from './Pages/Home/Home';

export default function App() {

  const Stack = createStackNavigator();
  const toastConfig = {
    error: (props) => (
      <ErrorToast
        {...props}
        text1Style={{
          fontSize: 10
        }}
        text2Style={{
          fontSize: 8
        }}
        text2NumberOfLines={2}
        style={{ borderLeftColor: "red" }}
      />
    )
  }
  return (
    <>
      <NavigationContainer>
        <Stack.Navigator initialRouteName="Home">
          <Stack.Screen name="Login" component={Login}></Stack.Screen>
          <Stack.Screen name="Home" component={Home}></Stack.Screen>
        </Stack.Navigator>
      </NavigationContainer>
      <Toast config={toastConfig} />
    </>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
