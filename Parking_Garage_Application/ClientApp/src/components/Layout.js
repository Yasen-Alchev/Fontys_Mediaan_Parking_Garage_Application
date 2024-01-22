import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

const containerStyle = {
    maxHeight: '100vh'
};

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
        <div style={containerStyle}>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
