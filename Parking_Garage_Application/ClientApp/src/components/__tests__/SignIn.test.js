import { render , screen , fireEvent , act } from '@testing-library/react';
import SignIn from '../SignIn';
import '@testing-library/jest-dom'
import { UserProvider , UserContext } from '../../contexts/UserContext';

global.google = {
    accounts: {
      id: {
        initialize: jest.fn(),
        renderButton: jest.fn(),
      },
    },
};

test('renders SignIn component', () => {
    render(<UserProvider><SignIn/></UserProvider>);
    const SignInComponent = screen.getByTestId('signInDiv');
    expect(SignInComponent).toBeInTheDocument();
});

test('renders Sign Out button when user is signed in', () => {
    const user =  { name: "Giorgio" };
    const setUser = { test: "test" };
    render(<UserContext.Provider value={{user,setUser}}><SignIn/></UserContext.Provider>);
    expect(screen.getByText('Sign Out')).toBeInTheDocument();
});

test('signs out when sign out button is clicked', () => {
    var user =  { name: "Giorgio" };
    var setUser = (test) => user = test;
    render(<UserContext.Provider value={{user,setUser}}><SignIn/></UserContext.Provider>);
    
    act(() => {
        fireEvent.click(screen.getByText('Sign Out'));
    });

    expect(user).toEqual({});
});