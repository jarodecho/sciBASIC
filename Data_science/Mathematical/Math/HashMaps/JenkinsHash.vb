Namespace google.pagerank

    ''' <summary>
    ''' Original source from http://256.com/sources/jenkins_hash_java/
    ''' 
    ''' <b>This is a Bob Jenkins hashing algorithm implementation</b>
    ''' &lt;br> 
    ''' These are functions for producing 32-bit hashes for hash table lookup.
    ''' hashword(), hashlittle(), hashlittle2(), hashbig(), mix(), and final()
    ''' are externally useful functions.  Routines to test the hash are included
    ''' if SELF_TEST is defined.  You can use this free for any purpose.  It's in
    ''' the public domain.  It has no warranty.
    ''' </summary>
    Public Class JenkinsHash

        ''' <summary>
        ''' max value to limit it to 4 bytes
        ''' </summary>
        Private Const MAX_VALUE As Long = &HFFFFFFFFL

        ' internal variables used in the various calculations
        Friend a As Long
        Friend b As Long
        Friend c As Long

        ''' <summary>
        ''' Convert a byte into a long value without making it negative. </summary>
        ''' <param name="b">
        ''' @return </param>
        Private Function byteToLong(b As SByte) As Long
            Dim val As Long = b And &H7F
            If (b And &H80) <> 0 Then val += 128
            Return val
        End Function

        ''' <summary>
        ''' Do addition and turn into 4 bytes. </summary>
        ''' <param name="val"> </param>
        ''' <param name="___add">
        ''' @return </param>
        Private Function add(val As Long, ___add As Long) As Long
            Return (val + ___add) And MAX_VALUE
        End Function

        ''' <summary>
        ''' Do subtraction and turn into 4 bytes. </summary>
        ''' <param name="val"> </param>
        ''' <param name="___subtract">
        ''' @return </param>
        Private Function subtract(val As Long, ___subtract As Long) As Long
            Return (val - ___subtract) And MAX_VALUE
        End Function

        ''' <summary>
        ''' Left shift val by shift bits and turn in 4 bytes. </summary>
        ''' <param name="val"> </param>
        ''' <param name="___xor">
        ''' @return </param>
        Private Function [xor](val As Long, [___xor] As Long) As Long
            Return (val Xor ___xor) And MAX_VALUE
        End Function

        ''' <summary>
        ''' Left shift val by shift bits.  Cut down to 4 bytes. </summary>
        ''' <param name="val"> </param>
        ''' <param name="shift">
        ''' @return </param>
        Private Function leftShift(val As Long, shift As Integer) As Long
            Return (val << shift) And MAX_VALUE
        End Function

        ''' <summary>
        ''' Convert 4 bytes from the buffer at offset into a long value. </summary>
        ''' <param name="bytes"> </param>
        ''' <param name="offset">
        ''' @return </param>
        Private Function fourByteToLong(bytes As SByte(), offset As Integer) As Long
            Return (byteToLong(bytes(offset + 0)) + (byteToLong(bytes(offset + 1)) << 8) + (byteToLong(bytes(offset + 2)) << 16) + (byteToLong(bytes(offset + 3)) << 24))
        End Function

        ''' <summary>
        ''' Mix up the values in the hash function.
        ''' </summary>
        Private Sub hashMix()
            a = subtract(a, b)
            a = subtract(a, c)
            a = [xor](a, c >> 13)
            b = subtract(b, c)
            b = subtract(b, a)
            b = [xor](b, leftShift(a, 8))
            c = subtract(c, a)
            c = subtract(c, b)
            c = [xor](c, (b >> 13))
            a = subtract(a, b)
            a = subtract(a, c)
            a = [xor](a, (c >> 12))
            b = subtract(b, c)
            b = subtract(b, a)
            b = [xor](b, leftShift(a, 16))
            c = subtract(c, a)
            c = subtract(c, b)
            c = [xor](c, (b >> 5))
            a = subtract(a, b)
            a = subtract(a, c)
            a = [xor](a, (c >> 3))
            b = subtract(b, c)
            b = subtract(b, a)
            b = [xor](b, leftShift(a, 10))
            c = subtract(c, a)
            c = subtract(c, b)
            c = [xor](c, (b >> 15))
        End Sub

        ''' <summary>
        ''' Hash a variable-length key into a 32-bit value.  Every bit of the
        ''' key affects every bit of the return value.  Every 1-bit and 2-bit
        ''' delta achieves avalanche.  The best hash table sizes are powers of 2.
        ''' </summary>
        ''' <param name="buffer">       Byte array that we are hashing on. </param>
        ''' <param name="initialValue"> Initial value of the hash if we are continuing from
        '''                     a previous run.  0 if none. </param>
        ''' <returns> Hash value for the buffer. </returns>
        Public Overridable Function hash(buffer As SByte(), initialValue As Long) As Long
            Dim len, pos As Integer

            ' set up the internal state
            ' the golden ratio; an arbitrary value
            a = &H9E3779B9L
            ' the golden ratio; an arbitrary value
            b = &H9E3779B9L
            ' the previous hash value
            c = &HE6359A60L

            ' handle most of the key
            pos = 0
            For len = buffer.Length To 12 Step -12
                a = add(a, fourByteToLong(buffer, pos))
                b = add(b, fourByteToLong(buffer, pos + 4))
                c = add(c, fourByteToLong(buffer, pos + 8))
                hashMix()
                pos += 12
            Next len

            c += buffer.Length

            ' all the case statements fall through to the next on purpose
            Select Case len
                Case 11
                    c = add(c, leftShift(byteToLong(buffer(pos + 10)), 24))

                Case 10
                    c = add(c, leftShift(byteToLong(buffer(pos + 9)), 16))

                Case 9
                    c = add(c, leftShift(byteToLong(buffer(pos + 8)), 8))
                    ' the first byte of c is reserved for the length

                Case 8
                    b = add(b, leftShift(byteToLong(buffer(pos + 7)), 24))

                Case 7
                    b = add(b, leftShift(byteToLong(buffer(pos + 6)), 16))

                Case 6
                    b = add(b, leftShift(byteToLong(buffer(pos + 5)), 8))

                Case 5
                    b = add(b, byteToLong(buffer(pos + 4)))

                Case 4
                    a = add(a, leftShift(byteToLong(buffer(pos + 3)), 24))

                Case 3
                    a = add(a, leftShift(byteToLong(buffer(pos + 2)), 16))

                Case 2
                    a = add(a, leftShift(byteToLong(buffer(pos + 1)), 8))

                Case 1
                    a = add(a, byteToLong(buffer(pos + 0)))
                    ' case 0: nothing left to add
            End Select

            hashMix()

            Return c
        End Function

        ''' <summary>
        ''' See hash(byte[] buffer, long initialValue)
        ''' </summary>
        ''' <param name="buffer"> Byte array that we are hashing on. </param>
        ''' <returns> Hash value for the buffer. </returns>
        Public Overridable Function hash(buffer As SByte()) As Long
            Return hash(buffer, 0)
        End Function
    End Class
End Namespace