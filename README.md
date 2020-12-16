# PaludariumController

## Introduction

PaludariumController is a hobby project of mine to control my arduino, which in turn can read the temperature and change the lights of my axolotl paludarium.
The plan is to use this software for all of my terrariums, paludariums, aquariums and what ever you can think of.


## Code Samples

Right now the main focus is the web API which is contained in the code.

As of this moment it is possible to get the temperature and control the lights from the web API.

### Temperature

For the temperature you can use Rest Get on the following Uri:  
https://host:portnumber/api/Temperature

The response will look as follows:
```json
 {
  "temperature": 19.56,
  "response": "Temperature: 19.56\r",
  "succes": true
}
```
### Lighting

For the lighting you've got two ways to approach this.

By Rest Get or Post.

#### Get

You can use get the current lighting by using Get on the following Uri:

https://host:portnumber/api/Light/

The result will look like this:

```json
{
  "red": "005",
  "blue": "025",
  "green": "000"
}
```

For chaning the lights you can use the folowing Uri's:

- https://host:portnumber/api/Light/SetLightsBy/CurrentLight
> CurrentLight uses the location and time of the user. For now this is only configurable in the code. 
- https://host:portnumber/api/Light/SetLightsBy/Hour/1?doFade=true
> Lights by hour can receive an int (where in the example 1 is used) and uses an optinal query for fading the lights
- https://host:portnumber/api/Light/SetLightsBy/RGB?r=1&g=1&b=1&doFade=true
> Lights by RGB recieves a query for the RGB values of R, G, B and als an optinal query for fading. If no numbers are given, it falls back to 0.
- https://host:portnumber/api/Light/SetLightsBy/Colorname/white?doFade=true
> Lights by color recieves color names. Fading is optinal. See for the full list of colornames https://htmlcolorcodes.com/color-names/ 

#### Post
One can also use to use the folowing uri to set the lights via a JSON object (see below) : 
https://host:portnumber/api/Light/

```json
{
  "red": "005",
  "blue": "025",
  "green": "000"
}
```
