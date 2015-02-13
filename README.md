SharpCircuit
=======

This is a C# port of Paul Falstad's excellent [circuit simulator](http://www.falstad.com/circuit/) applet. 

The library is in a very early stage. I've finished a first pass on all the original code, but there's still lots of work to be done so **expect changes to the core api**. 

Most of the circuit elements are untested. Initial testing suggests that the majority of the circuits work exactly as they did in the applet, but until further notice, your mileage may vary.

Licence: MIT/Boost C++

## ToDo

- Test everything!
- Momentary switches.

## Licence

The original applet source code [[download](http://www.falstad.com/circuit/src.zip)] is licensed under Paul Falstad's [Applet Source Licence](http://www.falstad.com/licensing.html). The new API and other improvements are licensed under the Boost Software License.

```
CirSim.java (c) 2010 by Paul Falstad - java@falstad.com
http://www.falstad.com/circuit/

Falstad.com Applet Source Licence.
http://www.falstad.com/licensing.html

You have permission to use these applets in a classroom setting or take screenshots 
as long as the applets are unmodified. Modification or redistribution for non-commercial 
purposes is allowed, as long as you credit me (Paul Falstad) and provide a link to my page 
(the page you found the applet(s) on, or http://www.falstad.com/mathphysics.html). Contact 
me for any other uses. The source code for each applet is generally available on that applet's 
web page, but some of the applets use third-party source code that has restrictions.

THIS SOFTWARE IS PROVIDED ``AS IS'' AND WITHOUT ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, 
WITHOUT LIMITATION, THE IMPLIED WARRANTIES OF MERCHANTIBILITY AND FITNESS FOR A PARTICULAR PURPOSE.
```

```
SharpCircuit (c) 2014 Riley 'Mervill' Godard - mervills.email@gmail.com
https://github.com/Mervill/SharpCircuit
http://transistorcollective.net/

Boost Software License - Version 1.0 - August 17, 2003
 
Permission is hereby granted, free of charge, to any person or organization
obtaining a copy of the software and accompanying documentation covered by
this license (the "Software") to use, reproduce, display, distribute,
execute, and transmit the Software, and to prepare [[derivative work]]s of the
Software, and to permit third-parties to whom the Software is furnished to
do so, all subject to the following:
 
The copyright notices in the Software and this entire statement, including
the above license grant, this restriction and the following disclaimer,
must be included in all copies of the Software, in whole or in part, and
all derivative works of the Software, unless such copies or derivative
works are solely in the form of machine-executable object code generated by
a source language processor.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT
SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE
FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.
```
